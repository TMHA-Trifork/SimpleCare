import React, { useState, useEffect } from "react";
import {
  EmergencyPatientListItem,
  transferEmergencyPatient,
} from "../api/emergencyPatients";
import { fetchRecipientWards, RecipientWard } from "../api/recipientWards";
import "./TransferMenu.css";

interface TransferMenuProps {
  patient: EmergencyPatientListItem;
  onClose: () => void;
  onTransferComplete: () => void;
}

const TransferMenu: React.FC<TransferMenuProps> = ({
  patient,
  onClose,
  onTransferComplete,
}) => {
  const [reason, setReason] = useState("");
  const [wardIdentifier, setWardIdentifier] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [wards, setWards] = useState<RecipientWard[]>([]);
  const [wardsLoading, setWardsLoading] = useState(true);
  const [wardsError, setWardsError] = useState<string | null>(null);

  useEffect(() => {
    setWardsLoading(true);
    fetchRecipientWards()
      .then(setWards)
      .catch(() => setWardsError("Failed to load wards"))
      .finally(() => setWardsLoading(false));
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsSubmitting(true);
    setError(null);

    try {
      await transferEmergencyPatient({
        patientId: patient.patientId,
        familyName: patient.familyName,
        givenNames: patient.givenNames,
        wardId: wardIdentifier,
        reason,
      });

      onTransferComplete();
    } catch (err) {
      setError("Failed to transfer patient. Please try again.");
      console.error("Transfer error:", err);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="transfer-menu-overlay">
      <div className="transfer-menu">
        <div className="transfer-menu-header">
          <h3>Transfer Patient to Bed Ward</h3>
          <button className="close-button" onClick={onClose}>
            ×
          </button>
        </div>

        <div className="patient-info">
          <p>
            <strong>Patient:</strong> {patient.givenNames} {patient.familyName}
          </p>
          <p>
            <strong>Status:</strong> {patient.status}
          </p>
        </div>

        {error && <div className="error-message">{error}</div>}

        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="wardIdentifier">Bed Ward</label>
            {wardsLoading ? (
              <div>Loading wards...</div>
            ) : wardsError ? (
              <div className="error-message">{wardsError}</div>
            ) : (
              <select
                id="wardIdentifier"
                value={wardIdentifier}
                onChange={(e) => setWardIdentifier(e.target.value)}
                required
              >
                <option value="" disabled>
                  Select a ward
                </option>
                {wards.map((ward) => (
                  <option key={ward.id} value={ward.id}>
                    {ward.name}
                  </option>
                ))}
              </select>
            )}
          </div>

          <div className="form-group">
            <label htmlFor="reason">Reason for Transfer</label>
            <textarea
              id="reason"
              value={reason}
              onChange={(e) => setReason(e.target.value)}
              required
              placeholder="Enter reason for transfer"
            />
          </div>

          <div className="button-group">
            <button type="button" className="cancel-button" onClick={onClose}>
              Cancel
            </button>
            <button
              type="submit"
              className="transfer-button"
              disabled={isSubmitting}
            >
              {isSubmitting ? "Transferring..." : "Transfer Patient"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default TransferMenu;
