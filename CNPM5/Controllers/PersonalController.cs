using Microsoft.AspNetCore.Mvc;
using CNPM5.Models;

public class PersonalController : Controller
{
    private readonly Cnpm5Context _context;

    public PersonalController(Cnpm5Context context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Lấy StudentId từ session (được lưu khi login)
        int? studentId = HttpContext.Session.GetInt32("StudentId");

        // Nếu chưa đăng nhập -> quay về login
        if (studentId == null)
        {
            return RedirectToAction("Index", "Login");
        }

        // Lấy thông tin sinh viên
        var student = _context.TblStudentss.FirstOrDefault(s => s.StudentId == studentId);

        if (student == null)
        {
            return NotFound("Không tìm thấy thông tin sinh viên!");
        }

        return View(student);
    }
}
