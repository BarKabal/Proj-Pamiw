using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proj.Infrastructure.Commands;
using Proj.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.WebAPI.Controllers
{
    [Route("[Controller]")]
    public class ApplyFormController : Controller
    {
        private readonly IApplyFormService _applyFormService;

        public ApplyFormController(IApplyFormService ApplyFormService)
        {
            _applyFormService = ApplyFormService;
        }

        // applyForm
        [HttpGet]
        public async Task<IActionResult> BrowseAll()
        {
            var z = await _applyFormService.BrowseAllAsync();
            return Json(z);
        }

        // applyForm/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplyForm(int id)
        {
            var ApplyForm = await _applyFormService.GetAsync(id);
            return Json(ApplyForm);
        }

        // applyForm
        [HttpPost]
        public async Task<IActionResult> AddApplyForm([FromBody] CreateApplyForm ApplyForm)
        {
            await _applyFormService.AddAsync(ApplyForm);
            return Created("", ApplyForm);
        }

        // applyForm/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditApplyForm([FromBody] UpdateApplyForm ApplyForm, int id)
        {
            await _applyFormService.UpdateAsync(id, ApplyForm);
            return NoContent();
        }

        // applyForm/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplyForm(int id)
        {
            await _applyFormService.DeleteAsync(id);
            return NoContent();
        }
    }
}
