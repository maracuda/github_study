﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyProjectOne.Models;

namespace StudyProjectOne.Repository
{
    /// <summary>
    /// Интерфейс репозитория
    /// </summary>
    interface IRepository
    {
        IEnumerable Read();
        void Update(ICollection<Prefix> collection_to_update);
        void Insert(IEnumerable collection_to_insert);
        void Remove(IEnumerable collection_to_remove);
    }
}
