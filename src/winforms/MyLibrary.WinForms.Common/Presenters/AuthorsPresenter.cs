using System;
using System.Collections.Generic;
using System.Linq;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Utilities;
using MyLibrary.WinForms.Common.Mvp;
using MyLibrary.WinForms.Common.Views;

namespace MyLibrary.WinForms.Common.Presenters;

public class AuthorsPresenter : BasePresenter<IRefView>
{
    private IMyLibraryDomainModel Model { get; set; }
    private IViewFactory ViewFactory { get; set; }
    // Авторы в том же порядке, в каком они показаны в представлении
    private List<Author> Authors { get; set; } = [];

    public AuthorsPresenter(IMyLibraryDomainModel domainModel, IRefView refView, IViewFactory viewFactory)
    {
        // Сохраняем внедряемые ссылки на модель, представление и фабрику представлений
        Model = domainModel;
        View = refView;
        ViewFactory = viewFactory;
        // Настраиваем представление
        View.Title = "Авторы";
        View.ColumnName = "Автор";
        // Подписываемся на события представления
        View.Loaded += OnLoaded;
        View.Add += OnAdd;
        View.Edit += OnEdit;
        View.Delete += OnDelete;
    }

    // Событие для оповещения подписчиков о том, что какой-либо автор был отредактирован
    // или удален. Нужно для немедленного обновления списка книг в главном окне.
    public event EventHandler Modified;

    private void ModifiedHandler()
    {
        Modified?.Invoke(this, EventArgs.Empty);
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        // Считываем список авторов
        UpdateView();
    }

    private void OnAdd(object sender, EventArgs e)
    {
        var refItemPresenter = new RefItemPresenter(ViewFactory.CreateRefItemView());
        refItemPresenter.View.Title = "Новый автор";
        refItemPresenter.View.Heading = "Информация о новом авторе";
        refItemPresenter.View.ItemLabel = "Автор:";

        if (refItemPresenter.View.ShowDialog())
        {
            try
            {
                var newAuthor = new Author { Id = 0, Name = refItemPresenter.View.ItemText };
                Model.AuthorRepository.Add(newAuthor);
            }
            catch (Exception ex)
            {
                View.ShowError("Произошла ошибка при добавлении нового автора.");
                Utils.Log.Trace(ex);
            }

            // Пересчитываем список авторов
            UpdateView();

            // Выделяем добавленного (последнего в списке) автора
            if (Authors.Count > 0)
                View.SelectItems([Authors.Count - 1]);
        }
        else
        {
            // Восстанавливаем фокус и выделение авторов
            View.FocusItemList();
        }
    }

    private void OnEdit(object sender, EventArgs e)
    {
        IReadOnlyList<int> selectedIndices = View.SelectedItemIndices;

        // Проверяем, есть ли хотя бы один выделенный элемент в списке авторов
        if (selectedIndices.Count == 0)
            return;

        // Если выделенных элементов несколько, то берем первый из них
        int selectedIndex = selectedIndices[0];
        Author author = Authors[selectedIndex];

        var refItemPresenter = new RefItemPresenter(ViewFactory.CreateRefItemView());
        refItemPresenter.View.Title = "Изменение существующего автора";
        refItemPresenter.View.Heading = "Информация об авторе";
        refItemPresenter.View.ItemLabel = "Автор:";
        refItemPresenter.View.ItemText = author.Name;

        if (refItemPresenter.View.ShowDialog())
        {
            try
            {
                author.Name = refItemPresenter.View.ItemText;
                Model.AuthorRepository.Update(author);
                View.UpdateItemAt(selectedIndex, author.Name);

                // Оповещаем подписчиков о том, что произошли изменения
                ModifiedHandler();
            }
            catch (Exception ex)
            {
                View.ShowError("Произошла ошибка при изменении информации об авторе.");
                Utils.Log.Trace(ex);
            }
        }
    }

    private void OnDelete(object sender, EventArgs e)
    {
        // Восстанавливаем фокус
        View.FocusItemList();

        IReadOnlyList<int> selectedIndices = View.SelectedItemIndices;

        if (selectedIndices.Count == 0 || !View.Confirm("Будут удалены не только эти авторы, но и все их книги.\n\nВы уверены, что хотите это всё удалить?",
            "Удаление авторов и всех их книг"))
            return;

        try
        {
            // Пробегаемся по списку выделенных элементов снизу вверх и удаляем их из модели и представления.
            // Если пробегаться по списку выделенных элементов сверху вниз, то после первого же удаления
            // все следующие элементы поднимутся вверх и список выделенных элементов станет не актуален.
            for (int i = selectedIndices.Count - 1; i >= 0; i--)
            {
                int index = selectedIndices[i];
                Model.AuthorRepository.Delete(Authors[index].Id);
                Authors.RemoveAt(index);
                View.RemoveItemAt(index);
            }

            // Оповещаем подписчиков о том, что произошли изменения
            ModifiedHandler();
        }
        catch (Exception ex)
        {
            View.ShowError("Произошла ошибка при удалении авторов.");
            Utils.Log.Trace(ex);
        }
    }

    // Заново считываем список из модели и показываем его в представлении
    private void UpdateView()
    {
        try
        {
            Authors = Model.AuthorRepository.GetAll().ToList();
            View.ShowItems(Authors.Select(a => a.Name));
        }
        catch (Exception ex)
        {
            View.ShowError("Произошла ошибка при получении списка авторов.");
            Utils.Log.Trace(ex);
        }
    }
}
