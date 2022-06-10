using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace ZendeskAPIUipath
{
    #region GetZendeskTicket
    public class GetZendeskTicket : CodeActivity
    {
        #region Parameters From UiPath Base
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Username")]
        public InArgument<string> Username { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Api Key")]
        public InArgument<string> ApiKey { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Base Url")]
        public InArgument<string> BaseURL { get; set; }

        [Category("Output")]
        [RequiredArgument]
        [Description("Request Output")]
        public OutArgument<string> RequestOutput { get; set; }
        #endregion

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter The ID of the ticket")]
        public InArgument<int> TicketId { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string userName = Username.Get(context) + "/token";
            string apiKey = ApiKey.Get(context);
            int ticketId = TicketId.Get(context);
            string baseURL = BaseURL.Get(context);

            try
            {
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri(baseURL + "/api/v2/tickets/" + ticketId);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(userName,apiKey);
                    var result = client.GetAsync(endpoint).Result;
                    var jsonOutput = result.Content.ReadAsStringAsync().Result;
                    
                    RequestOutput.Set(context, jsonOutput);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }
    }
    #endregion

    #region PostCreateTicket
    public class PostCreateTicket : CodeActivity
    {
        #region Parameters From UiPath Base
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Username")]
        public InArgument<string> Username { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Api Key")]
        public InArgument<string> ApiKey { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Base Url")]
        public InArgument<string> BaseURL { get; set; }

        [Category("Output")]
        [RequiredArgument]
        [Description("Request Output")]
        public OutArgument<string> RequestOutput { get; set; }
        #endregion

        #region Zendesk Ticke Model
        [Category("Input")]
        [Description("The agent currently assigned to the ticket")]
        public InArgument<int> Assignee_id { get; set; }

        [Category("Input")]
        [Description("The group this ticket is assigned to")]
        public InArgument<int> Group_id { get; set; }

        [Category("Input")]
        [Description("The organization of the requester. You can only specify the ID of an organization associated with the requester")]
        public InArgument<int> Organization_id { get; set; }

        [Category("Input")]
        [Description("The urgency with which the ticket should be addressed. Allowed values are urgent, high, normal, or low")]
        public InArgument<string> Priority { get; set; }

        [Category("Input")]
        [Description("The user who requested this ticket")]
        public InArgument<int> Requester_id { get; set; }

        [Category("Input")]
        [Description("The state of the ticket. Allowed values are new, open, pending, hold, solved, or closed")]
        public InArgument<string> Status { get; set; }

        [Category("Input")]
        [Description("The value of the subject field for this ticket")]
        public InArgument<string> Subject { get; set; }

        [Category("Input")]
        [Description("The user who submitted the ticket. The submitter always becomes the author of the first comment on the ticket")]
        public InArgument<int> Submitter_id { get; set; }

        [Category("Input")]
        [Description("The user who submitted the ticket. The submitter always becomes the author of the first comment on the ticket")]
        public InArgument<int> Ticket_form_id { get; set; }

        [Category("Input")]
        [Description("The type of this ticket. Allowed values are problem, incident, question, or task.")]
        public InArgument<string> Type { get; set; }

        [Category("Input")]
        [Description("The array of tags applied to this ticket")]
        public InArgument<List<string>> Tags { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Body of Comment")]
        public InArgument<string> Body { get; set; }
        #endregion


        protected override void Execute(CodeActivityContext context)
        {
            string userName = Username.Get(context) + "/token";
            string apiKey = ApiKey.Get(context);
            string baseURL = BaseURL.Get(context);

            int assigneeId = Assignee_id.Get(context);
            int groupId = Group_id.Get(context);
            int organizationId = Organization_id.Get(context);
            string priority = Priority.Get(context);
            int requesterId = Requester_id.Get(context);
            string status = Status.Get(context);
            string subject = Subject.Get(context);
            int submitterId = Submitter_id.Get(context);
            int ticketFormId = Ticket_form_id.Get(context);
            string type = Type.Get(context);
            List<string> tags = Tags.Get(context);
            string body = Body.Get(context);



            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(userName, apiKey);
                    var endpoint = new Uri(baseURL + "/api/v2/tickets");
                    Comment comment = new Comment() { body = body };
                    ZendeskTicketModel zendeskTicketModel = new ZendeskTicketModel()
                    {
                        assignee_id = assigneeId,
                        group_id = groupId,
                        organization_id = organizationId,
                        priority = priority,
                        requester_id = requesterId,
                        status = status,
                        subject = subject,
                        submitter_id = submitterId,
                        ticket_form_id = ticketFormId,
                        type = type,
                        tags = tags,
                        comment = comment,
                    };

                    TicketRoot ticketRoot = new TicketRoot()
                    {
                        ticket = zendeskTicketModel,
                    };
                    var newPostJson = JsonSerializer.Serialize(ticketRoot);
                    Console.WriteLine(newPostJson);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payload).Result;
                    var jsonOutput = result.Content.ReadAsStringAsync().Result;

                    RequestOutput.Set(context, jsonOutput);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }
    }
    #endregion

    #region PutUpdateTicket
    public class PutUpdateTicket : CodeActivity
    {
        #region Parameters From UiPath Base
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Username")]
        public InArgument<string> Username { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Api Key")]
        public InArgument<string> ApiKey { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Base Url")]
        public InArgument<string> BaseURL { get; set; }

        [Category("Output")]
        [RequiredArgument]
        [Description("Request Output")]
        public OutArgument<string> RequestOutput { get; set; }
        #endregion

        #region Zendesk Ticke Model
        [Category("Input")]
        [Description("The agent currently assigned to the ticket")]
        public InArgument<int> Assignee_id { get; set; }

        [Category("Input")]
        [Description("The group this ticket is assigned to")]
        public InArgument<int> Group_id { get; set; }

        [Category("Input")]
        [Description("The organization of the requester. You can only specify the ID of an organization associated with the requester")]
        public InArgument<int> Organization_id { get; set; }

        [Category("Input")]
        [Description("The urgency with which the ticket should be addressed. Allowed values are urgent, high, normal, or low")]
        public InArgument<string> Priority { get; set; }

        [Category("Input")]
        [Description("The user who requested this ticket")]
        public InArgument<int> Requester_id { get; set; }

        [Category("Input")]
        [Description("The state of the ticket. Allowed values are new, open, pending, hold, solved, or closed")]
        public InArgument<string> Status { get; set; }

        [Category("Input")]
        [Description("The value of the subject field for this ticket")]
        public InArgument<string> Subject { get; set; }

        [Category("Input")]
        [Description("The user who submitted the ticket. The submitter always becomes the author of the first comment on the ticket")]
        public InArgument<int> Submitter_id { get; set; }

        [Category("Input")]
        [Description("The user who submitted the ticket. The submitter always becomes the author of the first comment on the ticket")]
        public InArgument<int> Ticket_form_id { get; set; }

        [Category("Input")]
        [Description("The type of this ticket. Allowed values are problem, incident, question, or task.")]
        public InArgument<string> Type { get; set; }

        [Category("Input")]
        [Description("The array of tags applied to this ticket")]
        public InArgument<List<string>> Tags { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Body of Comment")]
        public InArgument<string> Body { get; set; }
        #endregion

        [Category("Input")]
        [RequiredArgument]
        [Description("The ID of the ticket")]
        public InArgument<int> Ticket_id { get; set; }


        protected override void Execute(CodeActivityContext context)
        {
            string userName = Username.Get(context) + "/token";
            string apiKey = ApiKey.Get(context);
            string baseURL = BaseURL.Get(context);

            int assigneeId = Assignee_id.Get(context);
            int groupId = Group_id.Get(context);
            int organizationId = Organization_id.Get(context);
            string priority = Priority.Get(context);
            int requesterId = Requester_id.Get(context);
            string status = Status.Get(context);
            string subject = Subject.Get(context);
            int submitterId = Submitter_id.Get(context);
            int ticketFormId = Ticket_form_id.Get(context);
            string type = Type.Get(context);
            List<string> tags = Tags.Get(context);
            string body = Body.Get(context);
            int ticketId = Ticket_id.Get(context);



            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(userName, apiKey);
                    var endpoint = new Uri(baseURL + "/api/v2/tickets/" + ticketId);
                    Console.WriteLine(endpoint);
                    Comment comment = new Comment() { body = body };
                    ZendeskTicketModel zendeskTicketModel = new ZendeskTicketModel()
                    {
                        assignee_id = assigneeId,
                        group_id = groupId,
                        organization_id = organizationId,
                        priority = priority,
                        requester_id = requesterId,
                        status = status,
                        subject = subject,
                        submitter_id = submitterId,
                        ticket_form_id = ticketFormId,
                        type = type,
                        tags = tags,
                        comment = comment,
                    };

                    TicketRoot ticketRoot = new TicketRoot()
                    {
                        ticket = zendeskTicketModel,
                    };
                    var newPostJson = JsonSerializer.Serialize(ticketRoot);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PutAsync(endpoint, payload).Result;
                    var jsonOutput = result.Content.ReadAsStringAsync().Result;

                    RequestOutput.Set(context, jsonOutput);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }
    }
    #endregion

    #region GetZendeskTicketComments
    public class GetZendeskTicketComments : CodeActivity
    {
        #region Parameters From UiPath Base
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Username")]
        public InArgument<string> Username { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Api Key")]
        public InArgument<string> ApiKey { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Base Url")]
        public InArgument<string> BaseURL { get; set; }

        [Category("Output")]
        [RequiredArgument]
        [Description("Request Output")]
        public OutArgument<string> RequestOutput { get; set; }
        #endregion

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter The ID of the ticket")]
        public InArgument<int> TicketId { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string userName = Username.Get(context) + "/token";
            string apiKey = ApiKey.Get(context);
            int ticketId = TicketId.Get(context);
            string baseURL = BaseURL.Get(context);

            try
            {
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri(baseURL + "/api/v2/tickets/"+ ticketId + "/comments");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(userName, apiKey);
                    var result = client.GetAsync(endpoint).Result;
                    var jsonOutput = result.Content.ReadAsStringAsync().Result;

                    RequestOutput.Set(context, jsonOutput);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }
    }
    #endregion

    #region SaveCommentAttachment
    public class SaveCommentAttachment : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter The ID of the ticket")]
        public InArgument<int> TicketId { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Attachment destination Directory Path.")]
        public InArgument<string> AttachmentDirectoryPath { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter downloaded File new name")]
        public InArgument<string> FileName { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter AttachmentUrl From Zendesk Ticket, content_url")]
        public InArgument<string> AttachmentUrl { get; set; }

        [Category("Output")]
        [RequiredArgument]
        [Description("Request Output with file path in format: date, fileName, TicketId ")]
        public OutArgument<string> OutputPath { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            int ticketId = TicketId.Get(context);
            string attachmentDirectoryPath = AttachmentDirectoryPath.Get(context);
            string fileName = FileName.Get(context);
            string attachmentUrl = AttachmentUrl.Get(context);           

            try
            {
                var filePath = attachmentDirectoryPath + "_" + DateTime.Now.ToString("ddMMMyyyy") + "_" + fileName + "_TicketId" + ticketId + ".xlsx";
                if (!Directory.Exists(attachmentDirectoryPath))
                {
                    Directory.CreateDirectory(attachmentDirectoryPath);
                }
                using (var client = new WebClient())
                {
                    var endpoint = new Uri(attachmentUrl);
                    client.DownloadFile(endpoint, filePath);
                }
                OutputPath.Set(context, filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }
    }
    #endregion

    #region GetTicketWithSelectedMID
    public class GetTicketWithSelectedMID : CodeActivity
    {
        #region Parameters From UiPath Base
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Username")]
        public InArgument<string> Username { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Api Key")]
        public InArgument<string> ApiKey { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Base Url")]
        public InArgument<string> BaseURL { get; set; }

        [Category("Output")]
        [RequiredArgument]
        [Description("Request Output")]
        public OutArgument<string> RequestOutput { get; set; }
        #endregion

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter The MID")]
        public InArgument<string> MID { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string userName = Username.Get(context) + "/token";
            string apiKey = ApiKey.Get(context);
            string mid = MID.Get(context);
            string baseURL = BaseURL.Get(context);

            try
            {
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri(baseURL + "/api/v2/search?query=/" + mid);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(userName, apiKey);
                    var result = client.GetAsync(endpoint).Result;
                    var jsonOutput = result.Content.ReadAsStringAsync().Result;

                    RequestOutput.Set(context, jsonOutput);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }
    }
    #endregion

    #region GetTicketsForSelectedView
    public class GetTicketsForSelectedView : CodeActivity
    {
        #region Parameters From UiPath Base
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Username")]
        public InArgument<string> Username { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Api Key")]
        public InArgument<string> ApiKey { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Base Url")]
        public InArgument<string> BaseURL { get; set; }

        [Category("Output")]
        [RequiredArgument]
        [Description("Request Output")]
        public OutArgument<string> RequestOutput { get; set; }
        #endregion

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter The ViewId")]
        public InArgument<string> ViewId { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string userName = Username.Get(context) + "/token";
            string apiKey = ApiKey.Get(context);
            string viewId = ViewId.Get(context);
            string baseURL = BaseURL.Get(context);

            try
            {
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri(baseURL + "/api/v2/views/"+viewId+"/tickets");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(userName, apiKey);
                    var result = client.GetAsync(endpoint).Result;
                    var jsonOutput = result.Content.ReadAsStringAsync().Result;

                    RequestOutput.Set(context, jsonOutput);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }
    }
    #endregion
}