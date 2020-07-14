using FMCW.Common.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worter.DTO.Language;
using Worter.HTTP;
using Worter.Services.Toast;

namespace Worter.Pages
{
    public class LearnBase : ComponentBase
    {
        [Inject] APIClient APIClient { get; set; }
        [Inject] ToastService toastService { get; set; }

        protected List<AnswerDTO> userAnswers = new List<AnswerDTO>();
        protected int learnIndex = 0;
        protected int idLanguage = 0;
        protected bool languageSelected = false;
        protected List<LanguageDTO> languages = new List<LanguageDTO>();
        protected List<LearnDTO> learns = new List<LearnDTO>();

        protected async Task LoadLanguages()
        {
            var apiRequest = Request.BuildGet("Language/GetAll");
            var response = await APIClient.Send<ListResult<LanguageDTO>>(apiRequest);
            if (response.Success)
            {
                languages = response.ResultOk;
            }
            else
            {
                toastService.ShowToast(response.ResultError.FriendlyErrorMessage, ToastLevel.Error);
            }
        }

        protected void ConfirmLanguage()
        {
            if (idLanguage == 0)
            {
                toastService.ShowError("Pick a language to learn");
                return;
            }

            languageSelected = true;
        }

        protected async Task StartLearn()
        {
            var url = $"Word/GetLearns?IdLanguage={idLanguage}";
            var apiRequest = Request.BuildGet(url);
            var response = await APIClient.Send<ListResult<LearnDTO>>(apiRequest);
            if (response.Success)
            {
                learns = response.ResultOk;
                learnIndex = 0;
                ShowWord();
            }
            else
            {
                toastService.ShowError(response.ResultError.FriendlyErrorMessage);
            }
        }

        protected void ShowWord()
        {
            userAnswers = new List<AnswerDTO>();
            for (int i = 0; i < learns[learnIndex].Translations.Count; i++)
            {
                userAnswers.Add(new AnswerDTO
                {
                    IsCorrect = false,
                    UserAnswer = ""
                });
            }
        }

        protected void CheckAnswer(AnswerDTO answer)
        {
            var actualLearn = learns[learnIndex];
            if (actualLearn.Translations.Contains(answer.UserAnswer) &&
                userAnswers.Count(a => answer.UserAnswer == a.UserAnswer) == 1)
            {
                answer.IsCorrect = true;
            }
            if (userAnswers.All(u => u.IsCorrect))
            {
                Console.WriteLine("LISTO PAPA LA QUE SIGUE");
            }
        }

        protected void Next()
        {
            learns[learnIndex].UserAnswers = userAnswers;
            learnIndex += 1;
            if (learnIndex == learns.Count)
            {
                // send data to API
            }
            else
            {
                ShowWord();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadLanguages();
        }
    }
}
