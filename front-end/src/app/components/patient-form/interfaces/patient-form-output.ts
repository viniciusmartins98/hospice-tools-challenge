import IPatientRequest from "../../../services/patients/interfaces/patient-request";

export default interface IPatientFormOutput extends IPatientRequest {
  patientId?: string | null;
}