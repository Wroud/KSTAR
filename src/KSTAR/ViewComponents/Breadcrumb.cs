using KSTAR.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSTAR.ViewComponents
{
    public struct BreadcumbRoute
    {
        public string Url;
        public string Name;
        public bool Active;
        public Apply _Name;
        public BreadcumbRoute(string name = "Index", string url = "/", Apply _name = null)
        {
            Name = name;
            Url = url;
            Active = false;
            _Name = _name;
        }
        public void Apply(ApplicationDbContext context, object value)
        {
            if (_Name != null)
            {
                _Name(ref this, context, value);
            }
        }
        public void SetActive()
        {
            Active = true;
        }
    }
    public delegate void Apply(ref BreadcumbRoute route, ApplicationDbContext context, object value);
    public class BreadcrumbViewComponent : ViewComponent
    {
        private static void Rli(ref BreadcumbRoute route, ApplicationDbContext context, object value)
        {
            route.Name = context.ApplicationRole.SingleOrDefault((r) => r.Id == (value as string)).Name;
        }
        private static void FSu(ref BreadcumbRoute route, ApplicationDbContext context, object value)
        {
            route.Name = value as string;
        }
        private static void FT(ref BreadcumbRoute route, ApplicationDbContext context, object value)
        {
            route.Name = context.ForumTopic.SingleOrDefault((r) => r.ID == int.Parse(value as string)).Title;
        }
        private static void DET(ref BreadcumbRoute route, ApplicationDbContext context, object value)
        {
            route.Name = context.ForumTopic.SingleOrDefault((r) => r.ID == int.Parse(value as string)).Title;
        }
        private static void DEG(ref BreadcumbRoute route, ApplicationDbContext context, object value)
        {
            route.Name = context.ForumGroup.SingleOrDefault((r) => r.ID == int.Parse(value as string)).Title;
        }
        private static void DES(ref BreadcumbRoute route, ApplicationDbContext context, object value)
        {
            route.Name = context.ForumSubject.SingleOrDefault((r) => r.ID == int.Parse(value as string)).Title;
        }
        private static Dictionary<string, BreadcumbRoute> Routes = new Dictionary<string, BreadcumbRoute>()
        {
            { "/Dashboard", new BreadcumbRoute(name:"Dashboard")},
            { "/Dashboard/RolesList", new BreadcumbRoute(name: "Список групп") },
            { "/Dashboard/RolesList/id", new BreadcumbRoute(
                name: "Участники группы",
                _name: Rli)
            },
            { "/Dashboard/AddRole", new BreadcumbRoute(name:"Добавить группу")},
            { "/Dashboard/UsersList", new BreadcumbRoute(name:"Список пользователей")},
            { "/Dashboard/AddUser", new BreadcumbRoute(name:"Добавить пользователя")},

            { "/Dashboard/ForumListGroup", new BreadcumbRoute(name: "Список групп форума") },
            { "/Dashboard/ForumAddGroup", new BreadcumbRoute(name: "Добавление группы форума") },
            { "/Dashboard/ForumEditGroup", new BreadcumbRoute(name: "Редактирование группы форума") },
            { "/Dashboard/ForumEditGroup/id", new BreadcumbRoute(_name: DEG) },
            { "/Dashboard/ForumListSubject", new BreadcumbRoute(name: "Список разделов форума") },
            { "/Dashboard/ForumAddSubject", new BreadcumbRoute(name: "Добавление раздела форума") },
            { "/Dashboard/ForumEditSubject", new BreadcumbRoute(name: "Редактирование раздела форума") },
            { "/Dashboard/ForumEditSubject/id", new BreadcumbRoute(_name: DES) },
            { "/Dashboard/ForumListTopic", new BreadcumbRoute(name: "Список тем форума") },
            { "/Dashboard/ForumAddTopic", new BreadcumbRoute(name: "Добавление темы форума") },
            { "/Dashboard/ForumEditTopic", new BreadcumbRoute(name: "Редактирование темы форума") },
            { "/Dashboard/ForumEditTopic/id", new BreadcumbRoute(_name:DET) },

            { "/Forum", new BreadcumbRoute(name:"Форум")},
            { "/Forum/subject", new BreadcumbRoute(name:"Форум",_name:FSu)},
            { "/Forum/subject/id", new BreadcumbRoute(name:"Форум",_name:FT)},
        };
        private ApplicationDbContext _context;
        public BreadcrumbViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var data = new List<BreadcumbRoute>();
            string path = "";
            string url = "";

            path += "/" + RouteData.Values["controller"] as string;
            url += "/" + RouteData.Values["controller"] as string;
            if (Routes.ContainsKey(path))
            {
                var r = Routes[path];
                r.Url = url;
                data.Add(r);
            }

            if (!RouteData.Values.ContainsKey("haction"))
            {
                path += "/" + RouteData.Values["action"] as string;
                url += "/" + RouteData.Values["action"] as string;
                if (Routes.ContainsKey(path))
                {
                    var r = Routes[path];
                    r.Url = url;
                    data.Add(r);
                }
            }

            foreach (var route in RouteData.Values)
            {
                switch (route.Key)
                {
                    case "controller":
                    case "action":
                        break;
                    default:
                        path += "/" + route.Key as string;
                        url += "/" + route.Value as string;
                        if (Routes.ContainsKey(path))
                        {
                            var r = Routes[path];
                            r.Url = url;
                            r.Apply(_context, route.Value);
                            data.Add(r);
                        }
                        break;
                }

            }
            if (data.Count > 0)
            {
                var ro = data[data.Count - 1];
                ro.Active = true;
                data[data.Count - 1] = ro;
            }
            return View(data);
        }
    }
}
