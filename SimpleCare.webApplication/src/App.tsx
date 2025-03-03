import {
  fetchEmergencyPatients,
  fetchEmergencyPatient,
  registerEmergencyPatient,
  transferEmergencyPatient,
} from "./api/emergencyPatients";
import "./App.css";

function App() {
  const testFetchPatients = async () => {
    try {
      const patients = await fetchEmergencyPatients();
      console.log("Fetched patients:", patients);
    } catch (error) {
      console.error("Error fetching patients:", error);
    }
  };

  const testFetchSinglePatient = async () => {
    try {
      // Using a test UUID - replace with actual ID when testing with real data
      const patient = await fetchEmergencyPatient(
        "00000000-0000-0000-0000-000000000000"
      );
      console.log("Fetched patient:", patient);
    } catch (error) {
      console.error("Error fetching patient:", error);
    }
  };

  const testRegisterPatient = async () => {
    try {
      await registerEmergencyPatient({
        personalIdentifier: "12345",
        familyName: "Test",
        givenNames: "Patient",
        reason: "Test registration",
      });
      console.log("Patient registered successfully");
    } catch (error) {
      console.error("Error registering patient:", error);
    }
  };

  const testTransferPatient = async () => {
    try {
      await transferEmergencyPatient({
        patientId: "00000000-0000-0000-0000-000000000000",
        familyName: "Test",
        givenNames: "Patient",
        wardIdentifier: "WARD1",
        reason: "Test transfer",
      });
      console.log("Patient transferred successfully");
    } catch (error) {
      console.error("Error transferring patient:", error);
    }
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>Simple Care</h1>
      </header>
      <div style={{ padding: "20px" }}>
        <button onClick={testFetchPatients} style={{ margin: "5px" }}>
          Test Fetch Patients
        </button>
        <button onClick={testFetchSinglePatient} style={{ margin: "5px" }}>
          Test Fetch Single Patient
        </button>
        <button onClick={testRegisterPatient} style={{ margin: "5px" }}>
          Test Register Patient
        </button>
        <button onClick={testTransferPatient} style={{ margin: "5px" }}>
          Test Transfer Patient
        </button>
      </div>
    </div>
  );
}

export default App;
