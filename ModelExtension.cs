using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Крутолапов
{
    public partial class RatingTennisistovEntities : DbContext
    {
        /// <summary>
        /// Статичная ссылка на контекст.
        /// </summary>
        private static RatingTennisistovEntities _context;

        /// <summary>
        /// Получение конекста.
        /// </summary>
        /// <returns>Контекст</returns>
        public static RatingTennisistovEntities GetContext()
        {
            if (_context == null)
                _context = new RatingTennisistovEntities();
            return _context;
        }
    }
}
