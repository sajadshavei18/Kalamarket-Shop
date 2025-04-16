using Kalamarket.Core.Viewmodel;
using Kalamarket.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kalamarket.Core.Service.Interface
{
    public interface IUserservice
    {
        int AddUser(user user);
        bool updateuser(user user);
        bool deleteuser(user user);
        bool ExistEmail(string email, int id);
        user Finduser(int userid, string Code);
        user FindUserbuyeEmail(string Email);
        user LoginUser(string email, string Password);
        informationAccountViewmodel informationAccount(int userid);
        user findEditUserbuyeid(int userid);
        edituserViewmodel finduserbuyeid(int userid);
        List<showfavoirateViewmodel> showfavoirateUser(int userid);
        List<showorderForUser> showorderForUsers(int userid);
        List<mycommentViewmodel> mycomment(int userid);
        List<ShowDetailorder> showDetailorders(int orderid);
    }
}
