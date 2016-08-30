using ManageSub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageSub.Interface
{
    public interface ICarteRepository
    {
        CarteModels findCarteByUser(string email);
    }
}