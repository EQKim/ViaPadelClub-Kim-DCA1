# Completed Feature Status

## Base Classes
* [X] ValueObject base
* [X] Entity base
* [X] AggregateRoot base


## UC1 - Register Player
* [X] Completed

**Description:**
A new player can be registered with a PlayerId and UniversityName.
The player should start as non-VIP and not banned.

**Value Objects:**
* [X] PlayerId
* [X] UniversityName

**Success Scenarios:**
* [X] S1 - Register player with valid PlayerId and UniversityName
  - Player is created
  - Player.Id is set
  - Player.UniversityName is set
  - Player.IsVip = false
  - Player.IsBanned = false

**Failure Scenarios:**
* [X] F1 - Register player with empty UniversityName
  - Operation should fail
  - Result.IsFailure = true


## UC2 - Grant VIP
* [x] Completed

**Description:**
A player can be granted VIP status, notes : Only Manager can execute this action

**Success Scenarios:**
* [x] S1 - Grant VIP to a non-VIP player
  - Operation should succeed
  - Player.IsVip becomes true

**Failure Scenarios:**
* [x] F1 - Grant VIP to a player who is already VIP
  - Operation should fail

## UC3 - Revoke VIP
* [x] Completed

**Description:**
A player's VIP status can be revoked.

**Success Scenarios:**
* [x] S1 - Revoke VIP from a VIP player
  - Player.IsVip becomes false

**Failure Scenarios:**
* [x] F1 - Revoke VIP from a non-VIP player
  - Operation should fail

## UC4 - Ban Player
* [x] Completed

**Description:** 
A player can be banned.
**Success Scenarios:**
* [x] S1 - Ban a player who is not already banned
  - Operation should succeed
  - Player.IsBanned becomes true

**Failure Scenarios:**
* [x] F1 - Ban a player who is already banned
  - Operation should fail

## UC5 - Create daily schedule
* [x] Completed

**Description:**
A daily schedule can be created with a manager and a time window.
A newly created daily schedule starts in Draft status.

**Value Objects:**
* [x] DailyScheduleId
* [x] ManagerId
* [x] TimeRange

**Success Scenarios:**
* [x] S1 - Create daily schedule with valid time range
  - Operation should succeed
  - DailySchedule is created
  - Status = Draft

**Failure Scenarios:**
* [x] F1 - Create daily schedule with end time before or equal to start time
  - Operation should fail

## UC6 - Activate daily schedule
* [x] Completed

**Description:**
A daily schedule in Draft status can be activated.

**Success Scenarios:**
* [x] S1 - Activate a draft daily schedule
  - Operation should succeed
  - Status becomes Active

**Failure Scenarios:**
* [x] F1 - Activate an already active daily schedule
  - Operation should fail

## UC7 - Create booking
* [X] Completed

**Description:**
A booking can be created for a player within a daily schedule.

**Value Objects:**
* [X] BookingId
* [X] CourtId
* [X] DailyScheduleCourtId
* [X] TimeRange

**Success Scenarios:**
* [X] S1 - Create booking for non-banned player within valid time slot
  - Operation should succeed
  - Booking is created

**Failure Scenarios:**
* [X] F1 - Create booking for banned player
* [X] F2 - Create booking outside daily schedule window
* [X] F3 - Create booking with overlapping time slot
* [X] F4 - Create booking on VIP-only court with non-VIP player

## UC8 - Cancel booking
* [x] Completed

**Description:**
An active booking can be cancelled.

**Success Scenarios:**
* [x] S1 - Cancel an active booking
  - Operation should succeed
  - Booking.Status becomes Cancelled

**Failure Scenarios:**
* [x] F1 - Cancel an already cancelled booking
  - Operation should fail