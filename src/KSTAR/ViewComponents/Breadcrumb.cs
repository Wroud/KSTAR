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
        public BreadcumbRoute(string name = "Index",string url="/", Apply _name = null)
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
            { "/Dashboard/AddUser", new BreadcumbRoute(name:"Добавить пользователя")}
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
            foreach (var route in RouteData.Values)
            {
                switch (route.Key)
                {
                    case "controller":
                    case "action":
                        path += "/" + route.Value as string;
                        url += "/" + route.Value as string;
                        if (Routes.ContainsKey(path))
                        {
                            var r = Routes[path];
                            r.Url = url;
                            data.Add(r);
                        }
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
            var ro = data[data.Count - 1];
            ro.Active = true;
            data[data.Count - 1] = ro;
            return View(data);
        }
    }
}
