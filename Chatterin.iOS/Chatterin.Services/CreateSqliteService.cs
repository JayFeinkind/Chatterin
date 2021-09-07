using System;
using System.Threading.Tasks;
using Chatterin.ClassLibrary;
using SQLite;

namespace Chatterin.Services
{
    public class CreateSqliteService
    {
        SQLiteAsyncConnection _context;

        public CreateSqliteService(FileService fileService)
        {
            _context = new SQLiteAsyncConnection(fileService.DbFilePath);
        }

        private async Task ClearDB()
        {
            var getTableNames = await _context.QueryAsync<TableWrapper>("SELECT name as TableName FROM sqlite_master WHERE type='table' AND name <> 'sqlite_sequence'");

            foreach (var table in getTableNames)
            {
                await _context.ExecuteAsync("drop table " + table.TableName);
            }

            getTableNames = await _context.QueryAsync<TableWrapper>("SELECT name as TableName FROM sqlite_master WHERE type='view'");

            foreach (var table in getTableNames)
            {
                await _context.ExecuteAsync("drop view " + table.TableName);
            }

            getTableNames = await _context.QueryAsync<TableWrapper>("SELECT name as TableName FROM sqlite_master WHERE type='index'");

            foreach (var table in getTableNames)
            {
                await _context.ExecuteAsync("drop index " + table.TableName);
            }
        }

        public async Task SetupDb()
        {
            await ClearDB();

            await _context.CreateTablesAsync(CreateFlags.None,
                typeof(User),
                typeof(Message),
                typeof(UserConversation),
                typeof(Conversation)
                );
        }





        private class TableWrapper
        {
            public string TableName { get; set; }
        }
    }
}
