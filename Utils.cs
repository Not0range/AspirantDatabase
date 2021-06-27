using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase
{
    public enum Role
    {
        Abiturient,
        Aspirant,
        EndEnducation,
        Admin
    }

    public enum UserDataState
    {
        AddedNotApproved,
        EditedNotApproved,
        Returned,
        Approved
    }

    public static class Utils
    {
        public static string GetString(this Role role)
        {
            switch(role)
            {
                case Role.Abiturient:
                    return "abiturient";
                case Role.Aspirant:
                    return "aspirant";
                case Role.EndEnducation:
                    return "endenducation";
                case Role.Admin:
                    return "admin";
            }
            return "";
        }
    }
}
