using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PipedriveNet.Dto;

namespace PipedriveNet.Endpoints
{
    public class ActivitesEndpoint
    {
        private readonly ApiClient _client;

        internal ActivitesEndpoint(ApiClient client)
        {
            _client = client;
        }

        public Task Delete(int id)
        {
            return _client.Delete("activities/" + id);
        }

        public Task<ActivityDto> Create(int dealId, string type, string subject, DateTime? due)
        {
            var activity = new JObject
            {
                ["deal_id"] = dealId,
                ["type"] = type,
                ["subject"] = subject
            };

            if (due.HasValue)
            {
                activity["due_date"] = due.Value.ToString("yyyy-MM-dd");
                activity["due_time"] = due.Value.ToString("hh:mm");
            }

            return _client.Post<ActivityDto>("activities", activity);
        }

        public Task<ActivityDto> CreateTask(int orgId, int personId, string subject, int userId, DateTime? due)
        {
            var task = new JObject
            {
                ["org_id"] = orgId,
                ["person_id"] = personId,
                ["user_id"] = userId,
                ["type"] = "task",
                ["subject"] = subject
            };

            if (due.HasValue)
            {
                task["due_date"] = due.Value.ToString("yyyy-MM-dd");
                task["due_time"] = due.Value.ToString("hh:mm");
            }

            return _client.Post<ActivityDto>("activities", task);
        }

        public Task<List<ActivityDto>> GetByDeal(int dealId)
        {
            return _client.Get<List<ActivityDto>>($"deals/{dealId}/activities?limit=9000");
        }
    }
}
