using FMCW.Common.Results;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worter.DTO.Language;
using Worter.HTTP;
using Worter.Services.Toast;

namespace Worter.Pages
{
    public class VocabularyBase : ComponentBase
    {
        [Inject] APIClient APIClient { get; set; }
        [Inject] ToastService toastService { get; set; }

        protected WordDTO newWord = new WordDTO();
        protected List<LanguageDTO> languages = new List<LanguageDTO>();
        protected WordFilterDTO filters = new WordFilterDTO();
        protected List<WordDTO> words = null;
        protected bool requestSent = false;
        protected bool requestGetSent = false;

        protected async void AddWord()
        {
            if (string.IsNullOrEmpty(newWord.OriginalMeaning))
            {
                toastService.ShowError("Complete original meaning field");
                return;
            }

            if (string.IsNullOrEmpty(newWord.TranslateMeaning))
            {
                toastService.ShowError("Complete translate meaning field");
                return;
            }

            if (newWord.IdLanguage == 0)
            {
                toastService.ShowError("Pick a language");
                return;
            }

            var apiRequest = Request.BuildPost("Word/Add", newWord);
            requestSent = true;
            var addRequest = await APIClient.Send<IntResult>(apiRequest);
            requestSent = false;

            if (addRequest.Success)
            {
                if (addRequest.ResultOperation == ResultOperation.RegisterAlreadyAdd)
                {
                    toastService.ShowWarning("Original and translate meaning already exists");
                }
                else
                {
                    toastService.ShowSucces("New word added correctly");
                }

                newWord.OriginalMeaning = "";
                newWord.TranslateMeaning = "";
                StateHasChanged();
            }
            else
            {
                toastService.ShowError("An error has ocurred");
            }
        }
        protected async Task LoadLanguages()
        {
            var apiRequest = Request.BuildGet("Language/GetAll");
            var response = await APIClient.Send<ListResult<Worter.DTO.Language.LanguageDTO>>(apiRequest);
            if (response.Success)
            {
                languages = response.ResultOk;
            }
            else
            {
                toastService.ShowToast(response.ResultError.FriendlyErrorMessage, ToastLevel.Error);
            }
        }

        protected async Task DeleteTranslate(int idTranslate)
        {
            Console.WriteLine(idTranslate);
        }

        protected async Task GetWords()
        {
            if (filters.IdLanguage == 0)
            {
                toastService.ShowError("Pick a language");
                return;
            }

            if (string.IsNullOrEmpty(filters.Filter) || filters.Filter.Length < 3)
            {
                toastService.ShowError("Type at least 3 chars before search");
                return;
            }

            var url = $"Word/Get?IdLanguage={filters.IdLanguage}&Filter={filters.Filter}";
            var request = Request.BuildGet(url);
            requestGetSent = true;
            var response = await APIClient.Send<ListResult<WordDTO>>(request);
            requestGetSent = false;
            if (response.Success)
            {
                words = response.ResultOk;
            }
            else
            {
                toastService.ShowError(response.ResultError.FriendlyErrorMessage);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            await LoadLanguages();
        }
    }
}
