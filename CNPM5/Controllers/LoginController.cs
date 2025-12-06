using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Dapper;
using CNPM5.Models;

namespace CNPM5.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        // Hiển thị form login
        public IActionResult Index()
        {
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            string connectionString = _config.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            string sql = @"
                SELECT LoginId, StudentId, Username 
                FROM tblLogin 
                WHERE Username = @Username 
                  AND Password = @Password
            ";

            var user = await conn.QueryFirstOrDefaultAsync<TblLogin>(
                sql, new { Username = username, Password = password });

            if (user != null)
            {
                ISession session = HttpContext.Session;
                session.SetString("UserName", user.Username);

                HttpContext.Session.SetInt32("StudentId", user.StudentId); ;

                return RedirectToAction("Index", "Personal");
            }

            ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
            return View();
        }
        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Register(TblLogin model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin!";
                return View();
            }

            string connectionString = _config.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);

            // Kiểm tra trùng username
            string checkSql = "SELECT COUNT(*) FROM tblLogin WHERE Username = @Username";
            int exists = await conn.ExecuteScalarAsync<int>(checkSql, new { model.Username });

            if (exists > 0)
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại!";
                return View();
            }

            string sql = @"
                INSERT INTO tblLogin (StudentId, Username, Password, createdAt, updatedAt)
                VALUES (@StudentId, @Username, @Password, GETDATE(), GETDATE());
            ";

            await conn.ExecuteAsync(sql, model);

            ViewBag.Success = "Đăng ký thành công! Hãy đăng nhập.";
            return RedirectToAction("Index");
        }
    }
}
