import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import {
  EmergencyPatient,
  fetchEmergencyPatient,
} from "../api/emergencyPatients";
import "./PatientDetails.css";

const PatientDetails: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [patient, setPatient] = useState<EmergencyPatient | null>(null);

  useEffect(() => {
    const loadPatient = async () => {
      try {
        if (id) {
          const data = await fetchEmergencyPatient(id);
          setPatient(data);
        }
      } catch (error) {
        console.error("Error fetching patient details:", error);
      }
    };

    loadPatient();
  }, [id]);

  if (!patient) {
    return <div>Loading patient details...</div>;
  }

  return (
    <>
      <h1 className="patient-details-title">Patient Details</h1>
      <div className="patient-details-container">
        <p>
          <strong>ID:</strong> {patient.patientId}
        </p>
        <p>
          <strong>Given Names:</strong> {patient.givenNames}
        </p>
        <p>
          <strong>Family Name:</strong> {patient.familyName}
        </p>
        <button
          className="back-button"
          onClick={() => navigate("/emergency-ward")}
        >
          Back
        </button>
      </div>
    </>
  );
};

export default PatientDetails;
