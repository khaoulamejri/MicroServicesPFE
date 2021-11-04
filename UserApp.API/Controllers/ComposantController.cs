using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;

namespace UserApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComposantController : ControllerBase
    {
        private readonly IComposantService ComposantService;

        public ComposantController(IComposantService composantService)
        {
            ComposantService = composantService;
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Add_ListComposants)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] Composant composant)
        {
            var cm = new Composant();
            var composants = ComposantService.GetAllComposants();
            try
            {
                if (composant.Type == "0")
                {
                    composant.Type = "List";
                }
                else if (composant.Type == "1")
                {
                    composant.Type = "Query";
                }
                else if (composant.Type == "2")
                {
                    composant.Type = "PowerBI";
                }
                if (ComposantService.CheckCombinationExist(composant.Type, composant.Action, composant.Module))
                {
                    return StatusCode(400, "combinationExistsAlready");
                }
                if (ComposantService.CheckComponentExist(composant.Name))
                {
                    return StatusCode(400, "componentExistsAlready");
                }

                var composantToAdd = new Composant { Module = composant.Module, Name = composant.Name, RedirectURL = composant.RedirectURL };

                if (composant.Type == "List")
                {
                    composantToAdd.Type = "List";
                    composantToAdd.Action = composant.Action;
                }
                else if (composant.Type == "Query")
                {
                    composantToAdd.Type = "Query";
                    composantToAdd.Action = composant.Action;
                }
                else if (composant.Type == "PowerBI")
                {
                    composantToAdd.Type = "PowerBI";
                    composantToAdd.Action = composant.Action;
                }
                cm = ComposantService.Create(composantToAdd);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
            return StatusCode(200, cm);
        }

       // [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Read_ListComposants)]
        [HttpGet, Route("GetComposantByID")]
        public IActionResult GetComposantByID(int id)
        {
            var composant = ComposantService.GetComposantByID(id);
            return StatusCode(200, composant);
        }

       // [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAllComposants")]
        public IActionResult GetAllComposants()
        {
            var Composants = ComposantService.GetAllComposants();
            return StatusCode(200, Composants);
        }

       // [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Edit_ListComposants)]
        [HttpPut, Route("Put")]
        public IActionResult Put(int id, [FromBody] Composant composant)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Model_Invalid");
            }
            var cm = new Composant();
            if (composant != null)
            {
                try
                {
                    if (ComposantService.CheckCombinationExistByID(composant.Type, composant.Action, composant.Module, id))
                    {
                        return StatusCode(400, "combinationExistsAlready");
                    }
                    if (ComposantService.CheckComponentExistByID(composant.Name, id))
                    {
                        return StatusCode(400, "componentExistsAlready");
                    }
                    var mycm = ComposantService.GetComposantByID(id);
                    if (mycm != null)
                    {
                        if (composant.Module != null) { mycm.Module = composant.Module; }
                        if (composant.Name != null) { mycm.Name = composant.Name; }
                        if (composant.Type != null)
                        {
                            mycm.Type = composant.Type;
                        }

                        if (composant.Action != null) { mycm.Action = composant.Action; }

                        cm = ComposantService.Edit(mycm);
                        if (cm == null)
                        {
                            return StatusCode(400, "Problème lors de modification du composant ");
                        }
                    }
                    else
                    {
                        return StatusCode(400, "composant existe");
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(400, e.Message);
                }
            }
            return StatusCode(200, cm);
        }

       // [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Delete_ListComposants)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var cm = ComposantService.Delete(id);
                if (cm == "no")
                {
                    return StatusCode(400, "FailDeleteProfile");
                }
                return StatusCode(200, cm);

            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}
