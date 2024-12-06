using EmployeeCURDCore.DAL;
using EmployeeCURDCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCURDCore.Controllers
{
    public class EmployeeController : Controller
    {
        /// <summary>
        /// object of DAL
        /// </summary>
        private readonly Employee_DAL _dal;
        public EmployeeController(Employee_DAL dal)
        {
            _dal = dal;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = _dal.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Data is not Valid";
                }
                bool result = _dal.Insert(model);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to Save";
                    return View();
                }
                TempData["successMessage"] = "Details Saved";
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                TempData["errorMessage"] = exception.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Employee employee = _dal.GetById(id);
                if (employee.ID == 0)
                {
                    TempData["errorMessage"] = $"Employee With Id : {id} not found";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception exception)
            {
                TempData["errorMessage"] = exception.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Data is not inValid";
                    return View();
                }
                bool result = _dal.Update(model);
                if (!result) 
                {
                    TempData["errorMessage"] = "Unable to update";
                    return View();
                }
                TempData["successMessage"] = "Details Updated";
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                TempData["errorMessage"] = exception.Message;
                return View();
            }
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            try
            {
                Employee employee = _dal.GetById(id);
                if (employee.ID == 0)
                {
                    TempData["errorMessage"] = $"Employee With Id : {id} not found";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception exception)
            {
                TempData["errorMessage"] = exception.Message;
                return View();
            }
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmation(Employee model)
        {
            try
            {
                bool result = _dal.Delete(model.ID);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to delete";
                    return View();
                }
                TempData["successMessage"] = "Details deleted";
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                TempData["errorMessage"] = exception.Message;
                return View();
            }
        }
        public IActionResult Details(int id)
        {
            try
            {
                Employee employee = _dal.GetById(id);
                if (employee.ID == 0)
                {
                    TempData["errorMessage"] = $"Employee With Id : {id} not found";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception exception)
            {
                TempData["errorMessage"] = exception.Message;
                return View();
            }
        }
    }
}
