# EF Core Migration Commands
# Run these from the solution root: c:\Projects\Repos\PigFarmManagement
#
# Common flags:
#   --project      = the project that contains the DbContext and Migrations folder
#   --startup-project = the runnable API project (reads appsettings / connection string)

$project        = "PigFarmManagement.Infrastructure"
$startupProject = "PigFarmManagement.Api"

# ── Add a new migration ──────────────────────────────────────────────────────
# Replace <MigrationName> with a descriptive PascalCase name, e.g. AddBatchIndex
# dotnet ef migrations add <MigrationName> --project $project --startup-project $startupProject

# ── Apply pending migrations to the database ─────────────────────────────────
# dotnet ef database update --project $project --startup-project $startupProject

# ── Roll back to a specific migration ────────────────────────────────────────
# dotnet ef database update <TargetMigrationName> --project $project --startup-project $startupProject

# ── Remove the last unapplied migration ──────────────────────────────────────
# dotnet ef migrations remove --project $project --startup-project $startupProject

# ── List all migrations and their applied status ─────────────────────────────
# dotnet ef migrations list --project $project --startup-project $startupProject

# ── Generate a SQL script for a migration range ──────────────────────────────
# Useful for reviewing what will run before touching the real database
# dotnet ef migrations script --project $project --startup-project $startupProject --output migration.sql

# =============================================================================
# QUICK SHORTCUTS — uncomment the line you need and run the script
# =============================================================================

# --- Apply the AddFarmIdIndexes migration (run this now) ---------------------
dotnet ef database update `
    --project $project `
    --startup-project $startupProject
