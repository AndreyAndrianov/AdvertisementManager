using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;

namespace AdverticementManager.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<string> CurrentUserRoles { get; set; }

        public User CurrentUser { get; set; }
    }
}
