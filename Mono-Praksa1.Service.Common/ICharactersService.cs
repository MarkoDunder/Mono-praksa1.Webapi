﻿using Mono_praksa1.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono_Praksa1.Service.Common
{
    public interface ICharactersService
    {
        Task<List<Character>> GetCharacters();
    }
}
