using Infrastructure.Domain.UnitOfWork;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataBaseService
{
    public static class DataBaseProviderSetUp
    {
        //SQLite In Memory
        private static readonly SqliteConnectionStringBuilder connectionStringBuilder =
            new SqliteConnectionStringBuilder { DataSource = ":memory:" };
        private static readonly SqliteConnection connection =
            new SqliteConnection(connectionStringBuilder.ToString());
        /// <summary>
        /// 
        /// </summary>
        public static DbContextOptions<UnitOfWorkContainer> SQLiteInMemoryOptions => 
            new DbContextOptionsBuilder<UnitOfWorkContainer>()
                .UseSqlite(connection)
                .Options;

        //EFCore In Memory
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DbContextOptions<UnitOfWorkContainer> SQLServerInMemoryOptions(string connectionString) => 
            new DbContextOptionsBuilder<UnitOfWorkContainer>()
            .UseInMemoryDatabase(connectionString)
            .Options;

    }
}
