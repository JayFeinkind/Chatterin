using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chatterin.ClassLibrary;
using SQLite;
using System.Linq;
using System.Linq.Expressions;

namespace Chatterin.Services
{
    public class ConversationService : ServiceBase<Conversation>, IConversationService
    {
        public ConversationService(
            ApiService apiService,
            FileService fileService,
            IConnectivityService connectivityService) : base(apiService, fileService, connectivityService)
        {
        }

        protected override string UrlAppendage => "Messages/conversations";

        protected override async Task<List<Conversation>> QueryResource(Expression<Func<Conversation, bool>> filter = null)
        {
            var conversations = await base.QueryResource(filter);
            var userConversations = await Db.Table<UserConversation>().ToListAsync();
            var messages = await Db.Table<Message>().ToListAsync();

            foreach (var convo in conversations)
            {
                convo.UserConversations = userConversations
                    .Where(uc => uc.ConversationId == convo.Id)
                    .Select(uc =>
                    {
                        uc.Messages = messages.Where(m => m.UserConversationId == uc.Id);
                        return uc;
                    });
            }

            return conversations;
        }

        protected override async Task InsertDependentResources(IEnumerable<Conversation> entities)
        {
            var userConversations = entities.SelectMany(e => e.UserConversations);
            var messages = userConversations.SelectMany(uc => uc.Messages);

            await Db.Table<UserConversation>().DeleteAsync(e => true);
            await Db.InsertAllAsync(userConversations);

            await Db.Table<Message>().DeleteAsync(e => true);
            await Db.InsertAllAsync(messages);
        }
    }
}
