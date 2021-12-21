#addin nuget:?package=Cake.Terraform&version=0.11.0

var target = Argument("target", "Default");
var migration = Argument("migration", "");
var configuration = Argument("configuration", "Release");

const string solution = "BituBooking";

var startupMigrationProj = "-s src/BituBooking.Api/BituBooking.Api.csproj ";
var migrationProj = "-p src/BituBooking.Infra.Storage.Postgres/BituBooking.Infra.Storage.Postgres.csproj ";
var projectsVariables = startupMigrationProj + migrationProj;

//////////////////////////////////////////////////////////////////////
// FUNCTIONS
//////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////
// TASKS
/////////////////////////////////////////////////////////////////////// 

var taskClean = Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
    CleanDirectory($"./src/**/bin");
});

var taskBuild = Task("build")
    .IsDependentOn("clean")
    .Does(() =>
{
    DotNetBuild($"./{solution}.sln", new DotNetCoreBuildSettings
    {
        Configuration = configuration,
    });
});


Task("database-clean")
    .Does(() =>
{
    var command = "ef database update 0 ";
    command += projectsVariables;
    command += "-v";

    DotNetTool(command);
});

var taskDatabaseUpdate = Task("database-update")
    .Does(() =>
{
    var command = "ef database update ";
    command += "--no-build ";
    command += projectsVariables;
    command += "-v";

    DotNetTool(command);
});

Task("migration-remove")
    .Does(() =>
{
    var command = "ef migrations remove ";
    command += projectsVariables;

    DotNetTool(command);
});

Task("migration-add")
    .IsDependentOn(taskBuild)
    .Does(() =>
{
    if(string.IsNullOrWhiteSpace(migration))
    {
        throw new ArgumentException("Migration is mandatory", nameof(migration));
    }

    var command = "ef migrations add ";
    command += "--no-build ";
    command += migration + " ";
    command += projectsVariables;

    DotNetTool(command);
});

var taskTerraformInit = Task("terraform-init")
    .Does(() =>
{
    // var configs = new Cake.Terraform.Init.TerraformInitSettings
    // {
    //     BackendConfigOverrides = new Dictionary<string, string>
    //     {
    //         {"chdir", "./deployments/terraform"}
    //     },
    //     ForceCopy = true
    // };

    // TerraformInit(configs);
    StartProcess("terraform", "-chdir=./deployments/terraform init");
});

var taskTerraformApply = Task("terraform-apply")
    .IsDependentOn(taskTerraformInit)
    .Does(() =>
{
    //  var settings = new Cake.Terraform.Apply.TerraformApplySettings
    //  {
    //     InputVariables = new Dictionary<string, string>
    //     {
    //         {"chdir", "./deployments/terraform"}
    //     }
    //  };

    //  TerraformApply(settings);
    StartProcess("terraform", "-chdir=./deployments/terraform apply -auto-approve");
});

var taskIntegrationTest = Task("dotnet-test")
    .IsDependentOn(taskBuild)
    .IsDependentOn(taskDatabaseUpdate)
    .IsDependentOn(taskTerraformApply)
    .Does(() =>
{
    DotNetTest("./tests/BituBooking.Integration/BituBooking.Integration.csproj");
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);