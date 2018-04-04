// --------------------------------------------------------------------------------------------------------------------
//   Copyright (c) Zaak (aaron.zaak@gmail.com). All rights reserved.  
//   Licensed under the Apache License 2.0. See LICENSE file in the project root for full license information.  
// --------------------------------------------------------------------------------------------------------------------

using System.Net;

using Microsoft.AspNetCore.Mvc;

namespace PropellerheadCustomers.DataService.Data
{
    public static class Responses
    {
        public static ActionResult GetAcceptedJsonResult(object dataPayload)
        {
            return new JsonResult(dataPayload) {StatusCode = (int) HttpStatusCode.Accepted};
        }

        public static ActionResult GetOkJsonResult(object dataPayload)
        {
            return new JsonResult(dataPayload) {StatusCode = (int) HttpStatusCode.OK};
        }
        public static ActionResult GetServerErrorJsonResult(object dataPayload)
        {
            return new JsonResult(dataPayload) {StatusCode = (int) HttpStatusCode.InternalServerError};
        }
    }
}