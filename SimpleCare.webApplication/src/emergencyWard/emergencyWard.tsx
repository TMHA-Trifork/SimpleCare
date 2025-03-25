import React, { useEffect, useState } from "react";
import {
  EmergencyPatientListItem,
  fetchEmergencyPatients,
  registerEmergencyPatient,
} from "../api/emergencyPatients";
import { useNavigate } from "react-router-dom";
import { formatStatusToDanish } from "../utils/formatters";
import "./emergencyWard.css";

const EmergencyWard: React.FC = () => {
  const [patients, setPatients] = useState<EmergencyPatientListItem[]>([]);
  const [isFormVisible, setIsFormVisible] = useState(false);
  const [formData, setFormData] = useState({
    givenNames: "",
    familyName: "",
    personalIdentifier: "",
    reason: "",
  });
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

  const handleFormToggle = () => {
    setIsFormVisible(!isFormVisible);
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleFormSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await registerEmergencyPatient(formData);
      const updatedPatients = await fetchEmergencyPatients();
      setPatients(updatedPatients);
      setFormData({
        givenNames: "",
        familyName: "",
        personalIdentifier: "",
        reason: "",
      });
      setIsFormVisible(false);
    } catch (error) {
      console.error("Error registering patient:", error);
    }
  };

  return (
    <div className="emergency-ward-container">
      <h1 className="emergency-ward-title">Emergency Ward</h1>
      <button className="register-button-toggle" onClick={handleFormToggle}>
        {isFormVisible ? "Close Form" : "Register Patient"}
      </button>
      {<div
        className={`register-form-container ${isFormVisible ? "visible" : ""}`}
      >
        <form onSubmit={handleFormSubmit} className="register-form">
          <div className="form-row">
            <input
              type="text"
              name="givenNames"
              placeholder="Given Names"
              value={formData.givenNames}
              onChange={handleInputChange}
              required
            />
            <input
              type="text"
              name="familyName"
              placeholder="Family Name"
              value={formData.familyName}
              onChange={handleInputChange}
              required
            />
          </div>
          <div className="form-row">
            <input
              type="text"
              name="personalIdentifier"
              placeholder="Personal Identifier"
              value={formData.personalIdentifier}
              onChange={handleInputChange}
              required
            />
            <input
              type="text"
              name="reason"
              placeholder="Reason"
              value={formData.reason}
              onChange={handleInputChange}
              required
            />
            <button type="submit" className="register-button">
              Register
            </button>
          </div>
        </form>
      </div>}
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
