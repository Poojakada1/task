using crude_operation.Data;
using crude_operation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace crude_operation.Controllers
{
    public class MyEmpController : Controller
    {
        private readonly MyDemo myDemo;

        public MyEmpController(MyDemo myDemo)
        { 
            this.myDemo = myDemo;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]

       public async Task<IActionResult> Add(AddEmployee e)
        {
            var emp = new Employee()
            {
                Id=Guid.NewGuid(),
               UserId=e.UserId,
                Name = e.Name,
                Address = e.Address,
                City = e.City,
                Department = e.Department,
                Email = e.Email,
                Salary = e.Salary,
                CreatedDate = e.CreatedDate,
            };
            await myDemo.Employees.AddAsync(emp);
            await myDemo.SaveChangesAsync();
            return RedirectToAction("Add");
        }
        public async Task<IActionResult>Index()
        {
            var emp = await myDemo.Employees.ToListAsync();

            return View(emp);
        }

        [HttpGet]
        public IActionResult DisplayById()
        {
            return View();
        }

        [HttpPost]
       
        public async Task<IActionResult> DisplayById(int eid,string sub)
        {
            if(sub == "Display")
            {
                var userdata=await myDemo.Employees.FirstOrDefaultAsync(x =>x.UserId==eid);
                return View(userdata);
            }
            else if(sub=="Delete")
            {
                var userdata=await myDemo.Employees.FirstOrDefaultAsync(x =>x.UserId == eid);
                if(userdata != null)
                {
                    myDemo.Employees.Remove(userdata);
                    await myDemo.SaveChangesAsync();
                    return RedirectToAction("DisplayById");
                }
                return View();
            }
            else
            {
                var userdata=await myDemo.Employees.FirstOrDefaultAsync(x=>x.UserId==eid);
                var e = new UpdateEmployee()
                {
                    Id = userdata.Id,
                    UserId = userdata.UserId,
                    Name = userdata.Name,
                    Address = userdata.Address,
                    City = userdata.City,
                    Department = userdata.Department,
                    Email = userdata.Email,
                    Salary = userdata.Salary,
                    CreatedDate = userdata.CreatedDate
                };
                return View("Edit1", e);
            }
        }
        public async Task<IActionResult> Update(UpdateEmployee e)
        {
            var emp = await myDemo.Employees.FirstOrDefaultAsync(x => x.UserId == e.UserId);
            if(emp != null)
            {
                emp.Name=e.Name;
                emp.Address=e.Address;
                emp.City=e.City;
                emp.Department=e.Department;
                emp.Email=e.Email;
                emp.Salary=e.Salary;
                emp.CreatedDate=e.CreatedDate;

                await myDemo.SaveChangesAsync();
                return RedirectToAction("DisplayById");
            }
            return View("DisplayById");
        }

        [HttpGet]

        public async Task<ActionResult>A_Delete(int id)
        {
            var userdata=await myDemo.Employees.FirstOrDefaultAsync(x=>x.UserId == id);
            if(userdata != null)
            {
                myDemo.Employees.Remove(userdata);
                await myDemo.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]

        public async Task<IActionResult>A_Edit(int id)
        {
            var userdata=await myDemo.Employees.FirstOrDefaultAsync(x=>x.UserId == id);
            var e = new UpdateEmployee()
            {
                UserId = userdata.UserId,
                Name = userdata.Name,
                Address = userdata.Address,
                City = userdata.City,
                Department = userdata.Department,
                Email = userdata.Email,
                Salary = userdata.Salary,
                CreatedDate = userdata.CreatedDate,

            };
            return await Task.Run(() => View("Edit", e));
        }
        [HttpPost]
        public async Task<IActionResult>A_Edit(UpdateEmployee e)
        {
            var user = await myDemo.Employees.FirstOrDefaultAsync(x => x.UserId == e.UserId);
            if (user != null)
            {
                user.Name = e.Name;
                user.Address = e.Address;
                user.City = e.City;
                user.Department = e.Department;
                user.Email = e.Email;
                user.Salary = e.Salary;
                user.CreatedDate = e.CreatedDate;

                await myDemo.Employees.AddAsync(user);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        

    }
}
