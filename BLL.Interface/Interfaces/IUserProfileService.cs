﻿using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Interfaces
{
    public interface IUserProfileService : IService<BllUserProfile>
    {
        IEnumerable<BllUserProfile> Search(BllUserProfile profile);
    }
}