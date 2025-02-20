namespace SimpleCare.BedWards.Domain;

public record Patient(Guid Id, string PersonalIdentifier, string FamilyName, string GivenNames);
