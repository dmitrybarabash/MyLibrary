using System;
using System.Collections.Generic;
using System.Linq;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Utilities;
using MyLibrary.WinForms.Common.Mvp;
using MyLibrary.WinForms.Common.Views;

namespace MyLibrary.WinForms.Common.Presenters;

public class PressesPresenter : BasePresenter<IRefView>
{
    private IMyLibraryDomainModel Model { get; set; }
    private IViewFactory ViewFactory { get; set; }
    // Издательства в том же порядке, в каком они показаны в представлении
    private List<Press> Presses { get; set; } = [];

    public PressesPresenter(IMyLibraryDomainModel domainModel, IRefView refView, IViewFactory viewFactory)
    {
        // Сохраняем внедряемые ссылки на модель, представление и фабрику представлений
        Model = domainModel;
        View = refView;
        ViewFactory = viewFactory;
        // Настраиваем представление
        View.Title = "Издательства";
        View.ColumnName = "Название";
        // Подписываемся на события представления
        View.Loaded += OnLoaded;
        View.Add += OnAdd;
        View.Edit += OnEdit;
        View.Delete += OnDelete;
    }

    // Событие для оповещения подписчиков о том, что какое-либо издательство было отредактировано
    // или удалено. Нужно для немедленного обновления списка книг в главном окне.
    public event EventHandler Modified;

    private void ModifiedHandler()
    {
        if (Modified is not null)
            Modified(this, EventArgs.Empty);
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        // Считываем список издательств
        UpdateView();
    }

    private void OnAdd(object sender, EventArgs e)
    {
        var refItemPresenter = new RefItemPresenter(ViewFactory.CreateRefItemView());
        refItemPresenter.View.Title = "Новое издательство";
        refItemPresenter.View.Heading = "Информация о новом издательстве";
        refItemPresenter.View.ItemLabel = "Издательство:";

        if (refItemPresenter.View.ShowDialog())
        {
            try
            {
                var newPress = new Press { Id = 0, Name = refItemPresenter.View.ItemText };
                Model.PressRepository.Add(newPress);
            }
            catch (Exception ex)
            {
                View.ShowError("Произошла ошибка при добавлении нового издательства.");
                Utils.Log.Trace(ex);
            }

            // Пересчитываем список издательств
            UpdateView();

            // Выделяем добавленное (последнее в списке) издательство
            if (Presses.Count > 0)
                View.SelectItems([Presses.Count - 1]);
        }
        else
        {
            // Восстанавливаем фокус и выделение издательств
            View.FocusItemList();
        }
    }

    private void OnEdit(object sender, EventArgs e)
    {
        IReadOnlyList<int> selectedIndices = View.SelectedItemIndices;

        // Проверяем, есть ли хотя бы один выделенный элемент в списке издательств
        if (selectedIndices.Count == 0)
            return;

        // Если выделенных элементов несколько, то берем первый из них
        int selectedIndex = selectedIndices[0];
        Press press = Presses[selectedIndex];

        var refItemPresenter = new RefItemPresenter(ViewFactory.CreateRefItemView());
        refItemPresenter.View.Title = "Изменение существующего издательства";
        refItemPresenter.View.Heading = "Информация об издательстве";
        refItemPresenter.View.ItemLabel = "Издательство:";
        refItemPresenter.View.ItemText = press.Name;

        if (refItemPresenter.View.ShowDialog())
        {
            try
            {
                press.Name = refItemPresenter.View.ItemText;
                Model.PressRepository.Update(press);
                View.UpdateItemAt(selectedIndex, press.Name);

                // Оповещаем подписчиков о том, что произошли изменения
                ModifiedHandler();
            }
            catch (Exception ex)
            {
                View.ShowError("Произошла ошибка при изменении информации об издательстве.");
                Utils.Log.Trace(ex);
            }
        }
    }

    private void OnDelete(object sender, EventArgs e)
    {
        // Восстанавливаем фокус
        View.FocusItemList();

        IReadOnlyList<int> selectedIndices = View.SelectedItemIndices;

        if (selectedIndices.Count == 0 || !View.Confirm("Будут удалены не только эти издательства, но и все изданные ими книги.\n\nВы уверены, что хотите это всё удалить?",
            "Удаление издательств и всех их книг"))
            return;

        try
        {
            // Пробегаемся по списку выделенных элементов снизу вверх и удаляем их из модели и представления.
            // Если пробегаться по списку выделенных элементов сверху вниз, то после первого же удаления
            // все следующие элементы поднимутся вверх и список выделенных элементов станет не актуален.
            for (int i = selectedIndices.Count - 1; i >= 0; i--)
            {
                int index = selectedIndices[i];
                Model.PressRepository.Delete(Presses[index].Id);
                Presses.RemoveAt(index);
                View.RemoveItemAt(index);
            }

            // Оповещаем подписчиков о том, что произошли изменения
            ModifiedHandler();
        }
        catch (Exception ex)
        {
            View.ShowError("Произошла ошибка при удалении издательств.");
            Utils.Log.Trace(ex);
        }

    }

    // Заново считываем список из модели и показываем его в представлении
    private void UpdateView()
    {
        try
        {
            Presses = Model.PressRepository.GetAll().ToList();
            View.ShowItems(Presses.Select(p => p.Name));
        }
        catch (Exception ex)
        {
            View.ShowError("Произошла ошибка при получении списка издательств.");
            Utils.Log.Trace(ex);
        }
    }
}
