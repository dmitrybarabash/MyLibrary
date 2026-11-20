using System;
using System.Collections.Generic;
using System.Linq;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Utilities;
using MyLibrary.Wpf.Common.Services;

namespace MyLibrary.Wpf.Common.ViewModels;

public class PressesViewModel : RefViewModel
{
    public PressesViewModel(IMyLibraryDomainModel domainModel, IDialogService dialogService)
        : base(domainModel, dialogService)
    {
        Title = "Издательства";
        ColumnName = "Название";
    }

    protected override void OnAdd()
    {
        var refItemViewModel = new RefItemViewModel
        {
            Title = "Новое издательство",
            Heading = "Информация о новом издательстве",
            ItemLabel = "Издательство:"
        };

        if (!DialogService.ShowDialog(refItemViewModel))
            return;

        try
        {
            var newPress = new Press { Id = 0, Name = refItemViewModel.ItemText };
            Model.PressRepository.Add(newPress);
        }
        catch (Exception ex)
        {
            DialogService.ShowError("Произошла ошибка при добавлении нового издательства.");
            Utils.Log.Trace(ex);
        }

        // Пересчитываем список издательств
        UpdateItems();

        // Выделяем добавленное (последнее в списке) издательство
        if (Items.Count > 0)
            SelectItems([Items[^1]]);
    }

    protected override void OnEdit()
    {
        // Если выделенных элементов несколько, то берем первый из них
        RefListItem item = GetSelectedItems().FirstOrDefault();
        if (item is null)
            return;

        var refItemViewModel = new RefItemViewModel
        {
            Title = "Изменение существующего издательства",
            Heading = "Информация об издательстве",
            ItemLabel = "Издательство:",
            ItemText = item.Name
        };

        if (!DialogService.ShowDialog(refItemViewModel))
            return;

        try
        {
            Model.PressRepository.Update(new Press { Id = item.Id, Name = refItemViewModel.ItemText });
            item.Name = refItemViewModel.ItemText;

            // Оповещаем подписчиков о том, что произошли изменения
            OnModified();
        }
        catch (Exception ex)
        {
            DialogService.ShowError("Произошла ошибка при изменении информации об издательстве.");
            Utils.Log.Trace(ex);
        }
    }

    protected override void OnDelete()
    {
        List<RefListItem> selectedItems = GetSelectedItems();

        if (selectedItems.Count == 0 ||
            !DialogService.Confirm(
                "Будут удалены не только эти издательства, но и все изданные ими книги.\n\nВы уверены, что хотите это всё удалить?",
                "Удаление издательств и всех их книг"))
            return;

        try
        {
            foreach (RefListItem item in selectedItems)
            {
                Model.PressRepository.Delete(item.Id);
                RemoveItem(item);
            }

            // Оповещаем подписчиков о том, что произошли изменения
            OnModified();
        }
        catch (Exception ex)
        {
            DialogService.ShowError("Произошла ошибка при удалении издательств.");
            Utils.Log.Trace(ex);
        }
    }

    protected override void UpdateItems()
    {
        try
        {
            ShowItems(Model.PressRepository.GetAll().Select(p => new RefListItem(p.Id, p.Name)).ToList());
        }
        catch (Exception ex)
        {
            DialogService.ShowError("Произошла ошибка при получении списка издательств.");
            Utils.Log.Trace(ex);
        }
    }
}
