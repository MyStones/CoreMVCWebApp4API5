using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVCWebApp4API5.Models;
using System.Net.Http;
using System.Net.Http.Headers;
namespace CoreMVCWebApp4API5.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetStudents()
        {
            List<Student> stds = new List<Student>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8088/api/student/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET - Get all the students
                var responseTask = client.GetAsync("getallstudents");
                // HTTP Get with id and name parameters
                //var responseTask = client.GetAsync(String.Format("values/getstudentname/?id={0}&name={1}", 25, "Stephen"));
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back student object list
                    var readTask = response.Content.ReadAsAsync<List<Student>>();
                    readTask.Wait();
                    stds = readTask.Result;
                }
            }
            return View(stds);
        }

        [HttpGet]
        public ActionResult GetStudentById(int StdId)
        {
            Student std = new Student();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8088/api/student/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET - Get a student by the Student Id
                var responseTask = client.GetAsync("getstudent/?stdId = {0}, StdId");
                // HTTP Get with id and name parameters
                //var responseTask = client.GetAsync(String.Format("getstudentByname/?id={0}&name={1}", StdId, StdName));
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back a single student object
                    var readTask = response.Content.ReadAsAsync<Student>();
                    readTask.Wait();
                    std = readTask.Result;
                }
            }
            return View(std);
        }

        public ActionResult AddStudent()
        {
            // Call the GetGradeList method of the API
            List<int> gl = GetGradeList();
            // Make a SelectList of the Gradelist and store it in a ViewBag
            ViewBag.grdList = new SelectList(gl);
	        
            return View();
        }


        [HttpPost]
        public ActionResult AddStudent(Student Std)
        {
            String res = "";
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8088/api/student");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //HTTP POST
                    var responseTask = client.PostAsJsonAsync(String.Format("PostStudent"), Std);
                    responseTask.Wait();

                    HttpResponseMessage response = responseTask.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsStringAsync();
                        readTask.Wait();
                        res = readTask.Result;
                    }

                }
                // If successfully Inserted the Student record
                if (Convert.ToInt32(res) > 0)
                    return RedirectToAction("GetStudents");
                else
                    // Something went wrong in the adding the record.
                    // Check the exception Log.
                    // Post back the student to the Edit View itself.
                    return View(Std);
            }
            else
                return View(Std);
        }

        public ActionResult EditStudent(int id)
        {
            Student std = new Student();
            // Get the Student object and pass it to the view, for Confirmation
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8088/api/student/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET - Get a student by the Student Id
                var responseTask = client.GetAsync("getstudent/?stdId = {0}, id");
                // HTTP Get with id and name parameters
                //var responseTask = client.GetAsync(String.Format("getstudentByname/?id={0}&name={1}", StdId, StdName));
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back a single student object
                    var readTask = response.Content.ReadAsAsync<Student>();
                    readTask.Wait();
                    std = readTask.Result;
                }
                return View(std);
            }
        }

        // Post back from the Edit Student View
        public ActionResult EditStudent(Student Std)
        {
            String res = "";
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8088/api/student");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //HTTP PUT, pass the Student Id and the Student Object
                    var responseTask = client.PutAsJsonAsync(String.Format("PutStudent/?id = {0}", Std.StudentId ), Std);
                    responseTask.Wait();

                    HttpResponseMessage response = responseTask.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsStringAsync();
                        readTask.Wait();
                        res = readTask.Result;
                    }

                }
                // If successfully Updated the Student record
                if (Convert.ToInt32(res) > 0)
                    return RedirectToAction("GetStudents");
                else
                    // Something went wrong in the adding the record.
                    // Check the exception Log.
                    // Post back the student to the Edit View itself.
                    return View(Std);
            }
            else
                return View(Std);
        }

        public ActionResult DeleteStudent(int id)
        {
            Student std = new Student();
            // Get the Student object and pass it to the view, for Confirmation
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8088/api/student/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HTTP GET - Get a student by the Student Id
                var responseTask = client.GetAsync("getstudent/?stdId = {0}, id");
                // HTTP Get with id and name parameters
                //var responseTask = client.GetAsync(String.Format("getstudentByname/?id={0}&name={1}", StdId, StdName));
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
                if (response.IsSuccessStatusCode)
                {
                    // Get back a single student object
                    var readTask = response.Content.ReadAsAsync<Student>();
                    readTask.Wait();
                    std = readTask.Result;
                }
                return View(std);
            }
        }

        // Post back from the DeleteConfirmation View
        [HttpPost, ActionName("DeleteStudent")]
        public ActionResult DeleteStudentConfirmed(int id)
        {
            String res = "";
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:8088/api/student");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //HTTP PUT, pass the Student Id and the Student Object
                    var responseTask = client.DeleteAsync(String.Format("DeleteStudent/?id={0}",id));
                    responseTask.Wait();

                    HttpResponseMessage response = responseTask.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsStringAsync();
                        readTask.Wait();
                        res = readTask.Result;
                    }

                }
                // If successfully Deleted the Student record
                if (Convert.ToInt32(res) > 0)
                    return RedirectToAction("GetStudents");
                else
                    // Something went wrong in the adding the record.
                    // Check the exception Log.
                    // Post back the student to the Delete View itself.
                    return View();
            }
            else
                return View();
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}
