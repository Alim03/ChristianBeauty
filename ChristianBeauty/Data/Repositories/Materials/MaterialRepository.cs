using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristianBeauty.Data.Context;
using ChristianBeauty.Data.Interfaces.Materials;
using ChristianBeauty.Models;

namespace ChristianBeauty.Data.Repositories.Materials
{
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        public MaterialRepository(ChristianBeautyDbContext context) : base(context) { }
    }
}
