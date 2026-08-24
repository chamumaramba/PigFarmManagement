# Pig Farm Management Requirements

## Purpose

Pig Farm Management is a farm operations system that manages pig farm activities across multiple farms, buildings, pens, animals, batches, breeding, health, nutrition, and financial reporting.

## Core Application Requirements

### Authentication and Authorization

- Register new users and manage passwords.
- Login with JWT bearer tokens.
- Refresh tokens with rotation and revoke support.
- Role-based authorization: Admin, FarmManager, and other roles as needed.
- Farm-scoped access using `FarmId` in user claims.

### Farm Management

- Create, update, activate, and deactivate farms.
- Retrieve farm details by id and by name.
- List all farms.
- Store farm metadata: name, location, currency, timezone.

### Building Management

- Create and configure buildings.
- Update building details.
- Retrieve buildings by id and by name.
- List all buildings.
- Activate and deactivate buildings.
- Buildings are linked to a farm.

### Pen Management

- Create pens within buildings.
- Update pen details.
- Retrieve pens by id.
- List all pens and list pens by building.
- Track pen capacity, occupancy, and type.

### Animal Management

- Create animals and manage animal data.
- Update animal records.
- Retrieve by id and by tag number.
- List animals.
- Filter animals by status, production stage, gender, and batch.
- Support sow/boar parent relationships.

### Batch Management

- Create and manage batches.
- Retrieve batch by id and by batch code.
- List all batches and filter by status.
- Link animals to batches.
- Support batch lifecycle operations.

### Breeding Management

- Record breeding events and pregnancy checks.
- Record farrowing results.
- Query breeding records by animal, sow, or boar.

### Health and Vaccination Management

- Create and update health records.
- Query health records by animal, type, and batch.
- Track vaccination schedules and treatments tied to stages.

### Animal Movement

- Track animal movements between pens or locations.
- Query movement history by animal.
- Support movement records for traceability.

## Nutrition and Feed Management

- Suggest feed type and quantity by animal stage.
- Provide nutrition recommendations by production stage.
- Recommend vaccination schedules by stage.
- Act as an assistant nutritionist to guide feeding and health interventions.
- Store feed programs and allocations for batches or animals if needed.

## Reporting and Financials

- Generate operational reports for farms, buildings, pens, animals, and batches.
- Provide nutrition reports and feed usage summaries.
- Create vaccination scheduling and compliance reports.
- Produce financial reports for farm operations.
- Track financial metrics such as feed cost, treatment cost, and revenue-related data.

## Data and Domain Requirements

- Use a domain model with entities for farms, buildings, pens, animals, batches, breeding records, health records, movements, feed programs, and financial data.
- Apply farm scoping logic to all relevant entities.
- Support soft delete via `IsDeleted`.
- Include audit and tracking fields where appropriate.

## API and Client

- Implement API endpoints for all core management features.
- Provide a client application for farm managers and admins.
- Keep business logic on the backend and use the frontend for data entry and display.

## Additional Notes

- The application should be extensible for future nutrition rule engines, stage-based recommendations, and financial analysis.
- The batch creation flow should be able to create animals and assign them to pens in one transactional workflow.
- The nutrition assistant features should integrate with batch and animal lifecycle staging.
