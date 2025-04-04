import { useEffect, useState } from "react";
import {
  fetchBedWardPatients,
  BedWardPatientListItem,
} from "../api/bedWardPatients";
import "./BedWard.css";

export const BedWard = () => {
  const [patients, setPatients] = useState<BedWardPatientListItem[]>([]);

  useEffect(() => {
    const loadPatients = async () => {
      try {
        const data = await fetchBedWardPatients();
        setPatients(data);
      } catch (error) {
        console.error("Error fetching bed ward patients:", error);
      }
    };

    loadPatients();
  }, []);

  return (
    <div className="bed-ward-container">
      <h1 className="bed-ward-title">Bed Ward</h1>
      <table className="bed-ward-table">
        <thead>
          <tr>
            <th>Given Names</th>
            <th>Family Name</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {patients.map((patient, index) => (
            <tr key={index} className="bed-ward-row">
              <td>{patient.givenNames}</td>
              <td>{patient.familyName}</td>
              <td>{patient.personalIdentifier}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};
