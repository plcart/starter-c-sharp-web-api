using System;

namespace Starter.Web.Api.Models
{
    public class BaseEntityModel
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public string CreatedFormatted { get { return Created.ToString("dd/MM/yyyy"); } }
    }
}