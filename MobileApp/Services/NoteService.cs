using Northboundei.Mobile.APIs;
using Northboundei.Mobile.Database;
using Northboundei.Mobile.Dto;
using Northboundei.Mobile.IServices;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteAPI _noteAPI;
        private readonly IServiceAuthService _childService;

        public NoteService(INoteAPI noteAPI, IServiceAuthService childService)
        {
            _noteAPI = noteAPI;
            _childService = childService;
        }

        public async Task<IEnumerable<SessionNoteData>> GetAllDrafts()
        {
            /*
             select *
            from session_note_tbl
            where Service_authorization
            in (select service_auth_tbl.service_authorization
                from service_auth_tbl
                where provider_npi = @NPI
                and GETDATE() >= serviceAuthStart
                and GETDATE() <= serviceAuthEnd)
            and session_date >= DATEADD(day,-1, GETDATE())
            and Draft_Final = 'Draft' 
             */
            try
            {
                // @NPI = SessionManager.UserInfo.NpiNo
                var notes = await GetNotesAsync();
                var childData = await _childService.GetServiceAuthDataAsync();
                var result = notes
                    .Where(x => x.DraftFinal == "Draft")
                    .Where(x =>
                    {
                        return childData!
                              .Where(child => child.ServiceAuthorization == x.ServiceAuthorization)
                              .Any(y => y.ProviderNpi == SessionManager.UserInfo.NpiNo
                                  && DateTime.Now >= DateTime.Parse(y.ServiceAuthStart)
                                  && DateTime.Now <= DateTime.Parse(y.ServiceAuthEnd));
                    })
                    .Where(x => DateTime.Parse(x.SessionDate) >= DateTime.Now.AddDays(-1))
                    .ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<int> GetTotalCount()
        {
            /*
             SQL Query
                select *
                from session_note_tbl
                where Service_authorization
                in (select service_auth_tbl.service_authorization
                    from service_auth_tbl
                    where provider_npi = @NPI 
                    and GETDATE() >= serviceAuthStart
                    and GETDATE() <= serviceAuthEnd)
                and (Draft_Final = 'Final' or Draft_Final like '%Check%')
             */
            try
            {
                // @NPI = SessionManager.UserInfo.NpiNo
                var notes = await GetNotesAsync();

                var result = notes
                    .Where(x => x.DraftFinal == "Final" || x.DraftFinal!.Contains("Check"))
                    .Count();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IEnumerable<SessionNoteData>> GetNotesAsync(bool Offline = true)
        {
            try
            {
                IEnumerable<SessionNoteData> result = 
                    Offline ? 
                    await DatabaseService.GetAllDataAsync<SessionNoteData>() : 
                    await _noteAPI.GetNotesAsync();

                var childData = await _childService.GetServiceAuthDataAsync(Offline);

                return result.Where(x =>
                {
                    return childData!
                          .Where(child => child.ServiceAuthorization == x.ServiceAuthorization)
                          .Any(y => y.ProviderNpi == SessionManager.UserInfo.NpiNo
                              && DateTime.Now >= DateTime.Parse(y.ServiceAuthStart)
                              && DateTime.Now <= DateTime.Parse(y.ServiceAuthEnd));
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
