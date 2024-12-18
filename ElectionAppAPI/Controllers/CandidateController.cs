using Dapper;
using ElectionAppAPI.models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElectionAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly SqlConnection conn = new SqlConnection(@"Data Source=PC-JUNEL-I7\SQLEXPRESS;Initial Catalog=ELECTION_DB;Integrated Security=True");
         
        [HttpGet]
        public IActionResult Get()
        {
            var result = conn.Query<CandidateModel>(@"
                SELECT * FROM [CANDIDATE_TABLE] ORDER BY [VOTE_COUNT] DESC
            ")?.ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CandidateModel model)
        {
            var result = conn.Query<CandidateModel>(@"
                INSERT INTO CANDIDATE_TABLE ([ID]
                  ,[LASTNAME]
                  ,[FIRSTNAME]
                  ,[BIRTHDATE]
                  ,[CANDIDATE_FOR]
                  ,[CREATED_AT]
                  ,[STATUS]
                  ,[VOTE_COUNT]
                  ,[COVER_LINKS]
                  ,[USER_LINKS])
                VALUES
                (@ID
                 ,@LASTNAME
                 ,@FIRSTNAME
                 ,@BIRTHDATE
                 ,@CANDIDATE_FOR
                 ,@CREATED_AT
                 ,@STATUS
                 ,@VOTE_COUNT
                 ,@COVER_LINKS
                 ,@USER_LINKS)
            ", new
            {
                ID = Guid.NewGuid(),
                LASTNAME = model.LASTNAME,
                FIRSTNAME = model.FIRSTNAME,
                BIRTHDATE = model.BIRTHDATE,
                CANDIDATE_FOR = model.CANDIDATE_FOR,
                CREATED_AT = DateTime.Now,
                STATUS = true,
                VOTE_COUNT = 0,
                COVER_LINKS = model.COVER_LINKS,
                USER_LINKS = model.USER_LINKS
            });
            return Ok();
        }
    
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            conn.Execute(@"DELETE FROM CANDIDATE_TABLE WHERE ID=@ID", new
            {
                ID = Guid.Parse(id)
            });
            return Ok();
        }
    
        [HttpPut("{id}")]
        public IActionResult Put(string id)
        {
            conn.Execute(@"
            UPDATE CANDIDATE_TABLE SET
            VOTE_COUNT=(VOTE_COUNT + 1)
            WHERE ID=@ID", new
            {
                ID = Guid.Parse(id)
            });
            return Ok();
        }
    }
}
