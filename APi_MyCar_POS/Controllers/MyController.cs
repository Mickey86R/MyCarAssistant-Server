using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using APi_MyCar_POS.Models;
//using APi_MyCar_POS.NewModels;

namespace APi_MyCar_POS.Controllers
{
    [Route("/MyCar_API/")]
    [ApiController]
    public class MyController : ControllerBase
    {
        MyContext db;

        public MyController(MyContext context)
        {
            this.db = context;
        }

        [HttpGet]
        public string Get()
        {
            string s = "Всё работает";
            /*
            s += "GetAutos/ - вывести список всех автомобилей\n";
            s += "GetAutoFromUser/{id} - вывести список автомобилей конкретного пользователя\n";
            s += "GetUsers/ - чтобы вывести список всех пользователей\n";
            s += "GetUser/{id} - вывести пользователя по его ID\n";
            s += "IsUserInDB/{login};{password} - проверить наличие пользователя в БД\n";
            s += "IsUserAvailable/{login} - проверить, есть ли пользователь стаким логином\n";
            s += "\n";
            s += "PostUser/{user} - добавить нового/редактировать пользователя\n";
            s += "PostAuto/{auto} - добавить/редактировать ААААААААВТОМОБИЛЬ!!!\n";
            s += "CreateRelation/{autoID};{userID} - Создать связь авто с владельцем\n";
            s += "PostExpens/{expens} - добавить/редактировать затрату\n";
            s += "PostTO/{tO} - добавить/редактировать затрату\n";
            s += "PostRecord/{record} - добавить/редактировать затрату\n";
            s += "\n";
            s += "PutUser/{id};{login};{password};{email} - изменить пользователя\n";
            s += "AddEmailForUser/{id};{email} - изменить email пользователя\n";
            s += "\n";
            s += "DelUser/{id} - удалить пользователя\n";
            s += "DelAuto/{id} - удалить автомобиль\n";
            */
            return s;
        }
        [Route("/MyCar_API/GetAutos")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auto>>> GetAutos()
        {
            List<Auto> autoList = new List<Auto>();
            List<Auto> list = this.db.Auto.ToList<Auto>();
            foreach (Auto auto1 in list)
            {
                Auto auto = auto1;
                auto.Records = this.db.Record.Where<Record>(r => r.AutoID == auto.ID).ToList<Record>();
                auto.Expenses = this.db.Expens.Where<Expens>(r => r.AutoID == auto.ID).ToList<Expens>();
                auto.T_Os = this.db.T_O.Where<T_O>(r => r.AutoID == auto.ID).ToList<T_O>();
            }
            return list;
        }

        [Route("/MyCar_API/GetAutoFromUser/{id}")]
        [HttpGet]
        public IEnumerable<AutoForApp> GetAutoFromUser(int id)
        {
            List<AutoForApp> resultAutos = new List<AutoForApp>();

            var autos = db.Auto.Where(a => a.Users.Any(u => u.ID == id)).ToList();

            foreach (Auto auto in autos)
            {
                auto.Records = this.db.Record.Where(r => r.AutoID == auto.ID).ToList();
                auto.Expenses = this.db.Expens.Where(r => r.AutoID == auto.ID).ToList();
                auto.T_Os = this.db.T_O.Where(r => r.AutoID == auto.ID).ToList();
                resultAutos.Add(auto.CovertToBase());
            }
            return resultAutos;
        }

        [Route("/MyCar_API/GetUsers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await this.db.Users.ToListAsync();
        }

        [Route("/MyCar_API/GetUser/{id}")]
        public async Task<ActionResult<User>> GetUserFromID(int id)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.ID == id); // Получить пользователя из БД с заданым id

            if (user == null)
                return NotFound();

            return user;
        }

        [HttpGet("/MyCar_API/IsUserInDB/{login};{password}")]
        public async Task<ActionResult<UserForApp>> IsUserInDB(string login, string password)
        {
            User tempUser = await db.Users.FirstOrDefaultAsync(x => x.Login == login); // Задание условия
            UserForApp user = new UserForApp();

            if (tempUser == null) return NotFound(); // Если пользователь не получен, значит нет такого логина

            if (tempUser.Password == password) // Если пароль введен верно, то конвертируем вытащенного пользователя для отправки
                user = tempUser.CovertToBase(); // иначе, если пароль не верен, то возвращаемый пользователь останется нулёвым

            return Ok(user); // возвращаем пользователя
        }

        [HttpGet("/MyCar_API/IsUserAvailable/{login}")]
        public async Task<bool> IsUserAvailable(string login)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.Login == login); // Задание условия

            if (user == null)
                return false;
            else
                return true;
        }

        [Route("/MyCar_API/DeleteAuto/{id}")]
        [HttpGet]
        public async Task<ActionResult<AutoForApp>> DeleteAuto(int id)
        {
            var auto = db.Auto.FirstOrDefault(a => a.ID == id);

            db.Auto.Remove(auto);
            db.SaveChanges();

            return Ok(auto.CovertToBase());
        }

        [HttpGet("/MyCar_API/DeleteExpens/{id}")]
        public async Task<ActionResult<Expens>> DeleteExpens(int id)
        {
            Expens expens = db.Expens.FirstOrDefault(x => x.ID == id);
            db.Expens.Remove(expens);

            await db.SaveChangesAsync();

            return Ok(expens);
        }

        [HttpGet("/MyCar_API/DeleteTO/{id}")]
        public async Task<ActionResult<T_O>> DeleteTO(int id)
        {
            T_O tO = db.T_O.FirstOrDefault(x => x.ID == id);
            db.T_O.Remove(tO);

            await db.SaveChangesAsync();

            return Ok(tO);
        }

        [HttpGet("/MyCar_API/DeleteRecord/{id}")]
        public async Task<ActionResult<Record>> DeleteRecord(int id)
        {
            Record record = db.Record.FirstOrDefault(x => x.ID == id);
            db.Record.Remove(record);

            await db.SaveChangesAsync();

            return Ok(record);
        }

        //--------------------------------------POST

        [HttpPost("/MyCar_API/PostUser/")]
        public async Task<ActionResult> PostUser(UserForApp newUser)
        {
            User user = db.Users.FirstOrDefault(x => x.ID == newUser.ID);

            if (user != null)
            {
                user.Update(newUser);
                db.Users.Update(user);
            }
            else
            {
                user = new User();
                user.Update(newUser);
                db.Users.Add(user);
            }

            db.SaveChanges();
            return Ok(user.CovertToBase());
        }

        [HttpPost("/MyCar_API/PostAuto/")]
        public async Task<ActionResult> PostAuto(AutoForApp newAuto)
        {
            Auto auto = db.Auto.FirstOrDefault(x => x.ID == newAuto.ID);

            if (auto != null)
            {
                auto.Update(newAuto);
                db.Auto.Update(auto);
            }
            else
            {
                auto = new Auto();
                auto.Update(newAuto);
                db.Auto.Add(auto);
            }

            db.SaveChanges();
            return Ok(auto.CovertToBase());
        }

        [HttpPost("/MyCar_API/CreateRelation/{autoID};{userID}")]
        public async Task<ActionResult> CreateRelation(int autoID, int userID)
        {
            User user = db.Users.FirstOrDefault(x => x.ID == userID);
            Auto auto = db.Auto.FirstOrDefault(x => x.ID == autoID);

            user.Autos.Add(auto);
            auto.Users.Add(user);

            db.SaveChanges();
            return Ok();
        }

        [HttpPost("/MyCar_API/PostExpens/")]
        public async Task<ActionResult> PostExpens(Expens newExpens)
        {
            Expens expens = db.Expens.FirstOrDefault(x => x.ID == newExpens.ID);

            if (expens != null)
            {
                expens.Update(newExpens);
                db.Expens.Update(expens);
            }
            else
            {
                expens = new Expens();
                expens.Update(newExpens);
                db.Expens.Add(expens);
            }

            db.SaveChanges();
            return Ok(expens);
        }

        [HttpPost("/MyCar_API/PostTO/")]
        public async Task<ActionResult> PostTO(T_O newTO)
        {
            T_O tO = db.T_O.FirstOrDefault(x => x.ID == newTO.ID);

            if (tO != null)
            {
                tO.Update(newTO);
                db.T_O.Update(tO);
            }
            else
            {
                tO = new T_O();
                tO.Update(newTO);
                db.T_O.Add(tO);
            }

            db.SaveChanges();
            return Ok(tO);
        }

        [HttpPost("/MyCar_API/PostRecord/")]
        public async Task<ActionResult> PostRecord(Record newRecord)
        {
            Record record = db.Record.FirstOrDefault(x => x.ID == newRecord.ID);

            if (record != null)
            {
                record.Update(newRecord);
                db.Record.Update(record);
            }
            else
            {
                record = new Record();
                record.Update(newRecord);
                db.Record.Add(record);
            }

            db.SaveChanges();
            return Ok(record);
        }

        //--------------------------------------PUT

        [HttpPut("/MyCar_API/PutUser/")]
        public async Task<ActionResult> PutUser(UserForApp newUser)
        {
            User user = db.Users.FirstOrDefault(x => x.ID == newUser.ID);
            user.Update(user);

            db.Users.Update(user);
            db.SaveChanges();

            return Ok(user.CovertToBase());
        }

        [HttpPut("/MyCar_API/PutAuto/")]
        public async Task<ActionResult> PutAuto(AutoForApp autoFromRequest)
        {
            Auto auto = await db.Auto.FirstOrDefaultAsync(x => x.ID == autoFromRequest.ID);
            auto.Update(autoFromRequest);

            db.Auto.Update(auto);
            await db.SaveChangesAsync();

            return Ok(auto.CovertToBase());
        }

        //--------------------------------------DELETE

        [HttpDelete("/MyCar_API/DelUser/{id}")]
        public async Task<ActionResult<User>> DelUser(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.ID == id);
            db.Users.Remove(user);

            await db.SaveChangesAsync();

            return Ok(user.CovertToBase());
        }

        [HttpDelete("/MyCar_API/DelAuto/{id}")]
        public async Task<ActionResult<Auto>> DelAuto(int id)
        {
            Auto auto = db.Auto.FirstOrDefault(x => x.ID == id);

            auto.AutoUsers.RemoveAll(x => x.AutoID == auto.ID);
            auto.Records.RemoveAll(x => x.ID == id);
            auto.T_Os.RemoveAll(x => x.ID == id);
            auto.Expenses.RemoveAll(x => x.ID == id);

            db.Auto.Remove(auto);

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("/MyCar_API/DelExpens/{id}")]
        public async Task<ActionResult<Expens>> DelExpens(int id)
        {
            Expens expens = db.Expens.FirstOrDefault(x => x.ID == id);
            db.Expens.Remove(expens);

            await db.SaveChangesAsync();

            return Ok(expens);
        }

        [HttpDelete("/MyCar_API/DelTO/{id}")]
        public async Task<ActionResult<T_O>> DelTO(int id)
        {
            T_O tO = db.T_O.FirstOrDefault(x => x.ID == id);
            db.T_O.Remove(tO);

            await db.SaveChangesAsync();

            return Ok(tO);
        }

        [HttpDelete("/MyCar_API/DelRecord/{id}")]
        public async Task<ActionResult<Record>> DelRecord(int id)
        {
            Record record = db.Record.FirstOrDefault(x => x.ID == id);
            db.Record.Remove(record);

            await db.SaveChangesAsync();

            return Ok(record);
        }
    }
}
