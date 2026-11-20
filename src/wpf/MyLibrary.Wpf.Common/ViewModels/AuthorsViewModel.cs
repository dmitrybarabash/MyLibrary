using System;
using System.Collections.Generic;
using System.Linq;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Utilities;
using MyLibrary.Wpf.Common.Services;

namespace MyLibrary.Wpf.Common.ViewModels;

public class AuthorsViewModel : RefViewModel
{
    public AuthorsViewModel(IMyLibraryDomainModel domainModel, IDialogService dialogService)
        : base(domainModel, dialogService)
    {
        Title = "Авторы";
        ColumnName = "Автор";
    }

    protected override void OnAdd()
    {
        var refItemViewModel = new RefItemViewModel
        {
            Title = "Новый автор",
            Heading = "Информация о новом авторе",
            ItemLabel = "Автор:"
        };

        if (!DialogService.ShowDialog(refItemViewModel))
            return;

        try
        {
            var newAuthor = new Author { Id = 0, Name = refItemViewModel.ItemText };
            Model.AuthorRepository.Add(newAuthor);
        }
        catch (Exception ex)
        {
            DialogService.ShowError("Произошла ошибка при добавлении нового автора.");
            Utils.Log.Trace(ex);
        }

        // Пересчитываем список авторов
        UpdateItems();

        // Выделяем добавленного (последнего в списке) автора
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
            Title = "Изменение существующего автора",
            Heading = "Информация об авторе",
            ItemLabel = "Автор:",
            ItemText = item.Name
        };

        if (!DialogService.ShowDialog(refItemViewModel))
            return;

        try
        {
            Model.AuthorRepository.Update(new Author { Id = item.Id, Name = refItemViewModel.ItemText });
            item.Name = refItemViewModel.ItemText;

            // Оповещаем подписчиков о том, что произошли изменения
            OnModified();
        }
        catch (Exception ex)
        {
            DialogService.ShowError("Произошла ошибка при изменении информации об авторе.");
            Utils.Log.Trace(ex);
        }
    }

    protected override void OnDelete()
    {
        List<RefListItem> selectedItems = GetSelectedItems();

        if (selectedItems.Count == 0 ||
            !DialogService.Confirm(
                "Будут удалены не только эти авторы, но и все их книги.\n\nВы уверены, что хотите это всё удалить?",
                "Удаление авторов и всех их книг"))
            return;

        try
        {
            foreach (RefListItem item in selectedItems)
            {
                Model.AuthorRepository.Delete(item.Id);
                RemoveItem(item);
            }

            // Оповещаем подписчиков о том, что произошли изменения
            OnModified();
        }
        catch (Exception ex)
        {
            DialogService.ShowError("Произошла ошибка при удалении авторов.");
            Utils.Log.Trace(ex);
        }
    }

    protected override void UpdateItems()
    {
        try
        {
            ShowItems(Model.AuthorRepository.GetAll().Select(a => new RefListItem(a.Id, a.Name)).ToList());
        }
        catch (Exception ex)
        {
            DialogService.ShowError("Произошла ошибка при получении списка авторов.");
            Utils.Log.Trace(ex);
        }
    }
}
