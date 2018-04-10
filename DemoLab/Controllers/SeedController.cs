#if DEBUG
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using DemoLab.Data.Access.Context;
using DemoLab.Data.Access.ExerciseExecutor;
using DemoLab.Data.Access.ExerciseManagement;
using DemoLab.Data.Access.UserManagement;
using DemoLab.Identity;
using DemoLab.Services.Identity;
using DemoLab.Services.Security;
using Microsoft.AspNet.Identity;
using Swashbuckle.Swagger.Annotations;

namespace DemoLab.Controllers
{
    /// <summary>
    /// Represents controller that is responsible for seeding and unseeding a database.
    /// </summary>
    [Authorize(Roles = GlobalInfo.Admin)]
    [RoutePrefix("api/v1/database")]
    public class SeedController : ApiController
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationIdentityDbContext _identityDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeedController"/> class.
        /// </summary>
        /// <param name="appDbContext">Instance of <see cref="ApplicationDbContext"/>.</param>
        /// <param name="userManager">Instance of <see cref="ApplicationUserManager"/>.</param>
        /// <param name="identityDbContext">Instance of <see cref="ApplicationIdentityDbContext"/>.</param>
        public SeedController(
            ApplicationDbContext appDbContext,
            ApplicationUserManager userManager,
            ApplicationIdentityDbContext identityDbContext)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _identityDbContext = identityDbContext ?? throw new ArgumentNullException(nameof(identityDbContext));
        }

        /// <summary>
        /// Seeds a database with data.
        /// </summary>
        /// <returns>Status of database seeding.</returns>
        [HttpPost]
        [Route("seed/all")]
        [ResponseType(typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public IHttpActionResult Seed()
        {
            AddCandidates();
            AddCandidateTaskResults();
            AddInvites();

            return Ok("Database was successfully seeded.");
        }

        /// <summary>
        /// Seeds a database with candidates.
        /// </summary>
        /// <param name="amount">The amount of candidates to seed.</param>
        /// <returns>Status of database seeding.</returns>
        /// <remarks>By default, an amount is 60.</remarks>
        // POST /api/v1/database/seed/candidates?amount=60
        [HttpPost]
        [Route("seed/candidates")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Parameter values are not correct")]
        public IHttpActionResult SeedCandidates([FromUri] int amount = 60)
        {
            try
            {
                if (amount < 1)
                {
                    return BadRequest($"{nameof(amount)} must be greater than 0.");
                }

                AddCandidates(amount);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok("Database was successfully seeded.");
        }

        /// <summary>
        /// Seeds a database with invites.
        /// </summary>
        /// <param name="amount">The amount of invites to seed.</param>
        /// <returns>Status of database seeding.</returns>
        /// <remarks>By default, an amount is 60.</remarks>
        // POST /api/v1/database/seed/invites?amount=60
        [HttpPost]
        [Route("seed/invites")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Parameter values are not correct")]
        public IHttpActionResult SeedInvites(int amount = 60)
        {
            try
            {
                if (amount < 1)
                {
                    return BadRequest($"{nameof(amount)} must be greater than 0.");
                }

                AddInvites(amount);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok("Database was successfully seeded.");
        }

        /// <summary>
        /// Seeds a database with candidate tasks results.
        /// </summary>
        /// <returns>Status of database seeding.</returns>
        /// <remarks>Note: candidates and candidate tasks must be already seeded.</remarks>
        // POST /api/v1/database/seed/taskResults
        [HttpPost]
        [Route("seed/taskResults")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public IHttpActionResult SeedCandidateTaskResults()
        {
            AddCandidateTaskResults();
            return Ok("Database was successfully seeded.");
        }

        /// <summary>
        /// Removes any data from database.
        /// </summary>
        /// <returns>Status of database unseeding.</returns>
        [HttpDelete]
        [Route("unseed/all")]
        [ResponseType(typeof(void))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public IHttpActionResult Unseed()
        {
            CleanCandidates();
            CleanCandidateTaskResults();
            CleanInvites();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Removes candidates from database.
        /// </summary>
        /// <returns>Status of database unseeding.</returns>
        [HttpDelete]
        [Route("unseed/candidates")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public IHttpActionResult UnseedCandidates()
        {
            try
            {
                CleanCandidates();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Removes invites from database.
        /// </summary>
        /// <returns>Status of database unseeding.</returns>
        [HttpDelete]
        [Route("unseed/invites")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public IHttpActionResult UnseedInvites()
        {
            try
            {
                CleanInvites();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Removes candidate task results from database.
        /// </summary>
        /// <returns>Status of database unseeding.</returns>
        [HttpDelete]
        [Route("unseed/taskResults")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public IHttpActionResult UnseedCandidateTaskResults()
        {
            CleanCandidateTaskResults();
            return StatusCode(HttpStatusCode.NoContent);
        }

        private void AddCandidates(int amount = 60)
        {
            var testCandidates = new Bogus.Faker<ApplicationUser>()
                .RuleFor(i => i.Email, f => f.Internet.ExampleEmail())
                .RuleFor(i => i.UserName, (f, i) => i.Email)
                .RuleFor(i => i.DomainId, Guid.NewGuid)
                .RuleFor(i => i.IsActive, true)
                .RuleFor(i => i.EmailConfirmed, true);

            var userProfiles = new Bogus.Faker<UserProfileInfo>()
                .RuleFor(i => i.FirstName, f => f.Name.FirstName())
                .RuleFor(i => i.LastName, f => f.Name.LastName())
                .Generate(amount);

            var candidateRole = _identityDbContext.Roles.FirstOrDefault(role =>
                role.Name.Equals(GlobalInfo.Candidate, StringComparison.Ordinal));

            int j = 0;
            foreach (var candidate in testCandidates.Generate(amount))
            {
                if (_userManager.CreateAsync(candidate, "1").Result == IdentityResult.Success)
                {
                    _userManager.AddToRoleAsync(candidate.Id, candidateRole.Name).Wait();
                    _userManager.AddProfileAsync(candidate.DomainId, userProfiles[j++]).Wait();
                }
            }

            _identityDbContext.SaveChanges();
            _appDbContext.SaveChanges();
        }

        private void AddInvites(int amount = 60)
        {
            var roles = new[] { GlobalInfo.Coach, GlobalInfo.Manager };
            var testInvites = new Bogus.Faker<Invite>()
                .RuleFor(i => i.Email, f => f.Internet.ExampleEmail())
                .RuleFor(i => i.RoleName, f => f.PickRandom(roles))
                .RuleFor(i => i.Token, Guid.NewGuid)
                .RuleFor(i => i.ExpiredDate, f => f.PickRandomParam(f.Date.Past(), f.Date.Recent(), f.Date.Soon(2)))
                .RuleFor(i => i.UserId, f => f.PickRandomParam(null, Guid.NewGuid().ToString()));

            _appDbContext.Invites.AddRange(testInvites.Generate(amount));
            _appDbContext.SaveChanges();
        }

        private void AddCandidateTaskResults()
        {
            var candidates = _identityDbContext.Users
               .Where(u => u.Roles
                   .Any(r => r.RoleId == _identityDbContext.Roles
                       .FirstOrDefault(s => s.Name == GlobalInfo.Candidate).Id));

            if (!candidates.Any())
            {
                throw new InvalidOperationException("No candidates exist.");
            }

            if (!_appDbContext.CandidateTasks.Any())
            {
                throw new InvalidOperationException("No tasks exist.");
            }

            foreach (var candidate in candidates)
            {
                int i = 0;
                var taskResults = new Bogus.Faker<CandidateTaskResult>()
                .RuleFor(r => r.CreatorId, candidate.DomainId)
                .RuleFor(r => r.ModifierId, candidate.DomainId)
                .RuleFor(r => r.CandidateExercise, f => _appDbContext.CandidateTasks.ToArray().ElementAt(i++))
                .RuleFor(r => r.UsedTipsNumber, (f, r) => f.Random.Number(((CandidateTask)r.CandidateExercise).Tips.Count()))
                .RuleFor(r => r.IsCompleted, true)
                .RuleFor(r => r.Score, (f, r) => r.CandidateExercise.MaximumScore - r.UsedTipsNumber)
                .RuleFor(r => r.Code, (f, r) => ((CandidateTask)r.CandidateExercise).CodeTemplate)
                .Generate(_appDbContext.CandidateTasks.Count());
                _appDbContext.CandidateTaskResults.AddRange(taskResults);
            }

            _appDbContext.SaveChanges();
        }

        private void CleanCandidates()
        {
            foreach (var user in _identityDbContext.Users
                .Where(u => u.Roles
                    .Any(r => r.RoleId.Equals(
                        _identityDbContext.Roles
                            .FirstOrDefault(s => s.Name
                                .Equals(GlobalInfo.Candidate, StringComparison.Ordinal)).Id, StringComparison.Ordinal))))
            {
                _identityDbContext.UserProfiles.Remove(_identityDbContext.UserProfiles.Find(user.Id));
                _identityDbContext.Users.Remove(user);
            }

            _identityDbContext.SaveChanges();
        }

        private void CleanCandidateTaskResults()
        {
            _appDbContext.CandidateTaskTips.RemoveRange(_appDbContext.CandidateTaskTips);
            _appDbContext.CandidateTaskResults.RemoveRange(_appDbContext.CandidateTaskResults);
            _appDbContext.Database.ExecuteSqlCommand("DBCC CHECKIDENT('dbo.CandidateTaskTips', RESEED, 0)");
            _appDbContext.Database.ExecuteSqlCommand("DBCC CHECKIDENT('dbo.CandidateExerciseResults', RESEED, 0)");

            _appDbContext.SaveChanges();
        }

        private void CleanInvites()
        {
            _appDbContext.Invites.RemoveRange(_appDbContext.Invites);
            _appDbContext.Database.ExecuteSqlCommand("DBCC CHECKIDENT('dbo.Invites', RESEED, 0)");

            _appDbContext.SaveChanges();
        }
    }
}
#endif