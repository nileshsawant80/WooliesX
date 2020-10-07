using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesX.TechChallenge.Api.Constants
{
    public static class ControllerRoute
    {
        public const string GetUser = ControllerName.User + nameof(GetUser);
        public const string RouteAnswersUser = "answers/user";

        public const string GetProducts = ControllerName.User + nameof(GetProducts);
        public const string RouteProducts = "sort/{sortOption?}";
    }
}
