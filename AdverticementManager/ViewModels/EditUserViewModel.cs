using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdverticementManager.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Roles { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }
    }
}
