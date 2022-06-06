# ZendeskAPIUipath

## GetZendeskTicket
    Show Ticket
    GET /api/v2/tickets/{ticket_id}

    Example Response
    Status 200 OK
    
    {
      "ticket": {
        "assignee_id": 235323,
        "collaborator_ids": [
          35334,
          234
        ],
        "created_at": "2009-07-20T22:55:29Z",
        "custom_fields": [
          {
            "id": 27642,
            "value": "745"
          },
          {
            "id": 27648,
            "value": "yes"
          }
        ],
        "description": "The fire is very colorful.",
        "due_at": null,
        "external_id": "ahg35h3jh",
        "follower_ids": [
          35334,
          234
        ],
        "group_id": 98738,
        "has_incidents": false,
        "id": 35436,
        "organization_id": 509974,
        "priority": "high",
        "problem_id": 9873764,
        "raw_subject": "{{dc.printer_on_fire}}",
        "recipient": "support@company.com",
        "requester_id": 20978392,
        "satisfaction_rating": {
          "comment": "Great support!",
          "id": 1234,
          "score": "good"
        },
        "sharing_agreement_ids": [
          84432
        ],
        "status": "open",
        "subject": "Help, my printer is on fire!",
        "submitter_id": 76872,
        "tags": [
          "enterprise",
          "other_tag"
        ],
        "type": "incident",
        "updated_at": "2011-05-05T10:38:52Z",
        "url": "https://company.zendesk.com/api/v2/tickets/35436.json",
        "via": {
          "channel": "web"
        }
      }
    }
## PostCreateTicket
    Create Ticket
    POST /api/v2/tickets
    Status 201 Created
    
    {
      "ticket": {
        "assignee_id": 235323,
        "collaborator_ids": [
          35334,
          234
        ],
        "created_at": "2009-07-20T22:55:29Z",
        "custom_fields": [
          {
            "id": 27642,
            "value": "745"
          },
          {
            "id": 27648,
            "value": "yes"
          }
        ],
        "description": "The fire is very colorful.",
        "due_at": null,
        "external_id": "ahg35h3jh",
        "follower_ids": [
          35334,
          234
        ],
        "group_id": 98738,
        "has_incidents": false,
        "id": 35436,
        "organization_id": 509974,
        "priority": "high",
        "problem_id": 9873764,
        "raw_subject": "{{dc.printer_on_fire}}",
        "recipient": "support@company.com",
        "requester_id": 20978392,
        "satisfaction_rating": {
          "comment": "Great support!",
          "id": 1234,
          "score": "good"
        },
        "sharing_agreement_ids": [
          84432
        ],
        "status": "open",
        "subject": "Help, my printer is on fire!",
        "submitter_id": 76872,
        "tags": [
          "enterprise",
          "other_tag"
        ],
        "type": "incident",
        "updated_at": "2011-05-05T10:38:52Z",
        "url": "https://company.zendesk.com/api/v2/tickets/35436.json",
        "via": {
          "channel": "web"
        }
      }
    }
## PutUpdateTicket
    Update Ticket
    PUT /api/v2/tickets/{ticket_id}
    Example Response
    Status 200 OK
    
    {
      "audit": {
        "events": [
          {
            "field_name": "subject",
            "id": 206091192907,
            "type": "Create",
            "value": "My printer is on fire!"
          },
          {
            "body": "The smoke is very colorful.",
            "id": 206091192547,
            "type": "Comment"
          }
        ]
      },
      "ticket": {
        "id": 35436,
        "requester_id": 123453,
        "status": "open",
        "subject": "My printer is on fire!"
      }
    }
## GetZendeskTicketComments
    List Comments
    GET /api/v2/tickets/{ticket_id}/comments
    Returns the comments added to the ticket.

    Example Response
    Status 200 OK
    
    {
      "comments": [
        {
          "attachments": [
            {
              "content_type": "text/plain",
              "content_url": "https://company.zendesk.com/attachments/crash.log",
              "file_name": "crash.log",
              "id": 498483,
              "size": 2532,
              "thumbnails": []
            }
          ],
          "author_id": 123123,
          "body": "Thanks for your help!",
          "created_at": "2009-07-20T22:55:29Z",
          "id": 1274,
          "metadata": {
            "system": {
              "client": "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36",
              "ip_address": "1.1.1.1",
              "latitude": -37.000000000001,
              "location": "Melbourne, 07, Australia",
              "longitude": 144.0000000000002
            },
            "via": {
              "channel": "web",
              "source": {
                "from": {},
                "rel": "web_widget",
                "to": {}
              }
            }
          },
          "public": true,
          "type": "Comment"
        }
      ]
    }
## SaveCommentAttachment
    
## GetTicketWithSelectedMID
    /api/v2/search.json?query={search_string}
## GetTicketsForSelectedView
    List Tickets From a View
    GET /api/v2/views/{view_id}/tickets
    Example Response
    Status 200 OK
    
    {
      "tickets": [
        {
          "id": 35436,
          "requester_id": 20978392,
          "subject": "Help I need somebody!"
        },
        {
          "id": 20057623,
          "requester_id": 20978392,
          "subject": "Not just anybody!"
        }
      ]
    }

