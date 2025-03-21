import React, { useEffect, useState } from "react";
import {
  EmergencyPatientListItem,
  fetchEmergencyPatients,
} from "../api/emergencyPatients";
import { useNavigate } from "react-router-dom";
import { formatStatusToDanish } from "../utils/formatters";
import "./emergencyWard.css";

const EmergencyWard: React.FC = () => {
  const [patients, setPatients] = useState<EmergencyPatientListItem[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    const loadPatients = async () => {
      try {
        const data = await fetchEmergencyPatients();
        setPatients(data);
      } catch (error) {
        console.error("Error fetching emergency patients:", error);
      }
    };

    loadPatients();
  }, []);

  const handleViewDetails = (id: string) => {
    navigate(`/emergency-ward/${id}`);
  };

  return (
    <div className="emergency-ward-container">
      <h1 className="emergency-ward-title">Emergency Ward</h1>
      <table className="emergency-ward-table">
        <thead>
          <tr>
            <th>Given Names</th>
            <th>Family Name</th>
            <th>Status</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {patients.map((patient) => (
            <tr key={patient.patientId} className="emergency-ward-row">
              <td>{patient.givenNames}</td>
              <td>{patient.familyName}</td>
              <td>{formatStatusToDanish(patient.status)}</td>
              <td>
                <button
                  className="view-details-button"
                  onClick={() => handleViewDetails(patient.patientId!)}
                >
                  View Details
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default EmergencyWard;
