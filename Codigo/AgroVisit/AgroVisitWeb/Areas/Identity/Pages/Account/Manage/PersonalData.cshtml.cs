// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using AgroVisitWeb.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AgroVisitWeb.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {

        /// <summary>
        /// private readonly ILogger<PersonalDataModel> _logger;
        /// </summary>
        private readonly UserManager<UsuarioIdentity> _userManager;

        public PersonalDataModel(
            UserManager<UsuarioIdentity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }
}
