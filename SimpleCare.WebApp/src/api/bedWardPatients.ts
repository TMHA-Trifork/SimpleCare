import { components } from '../types/api-types'
import { getApiUrl } from './config'

export type BedWardPatientListItem = components['schemas']['BedWardPatientListItem']
export type BedWardPatient = components['schemas']['BedWardPatient']
export type PatientAdmission = components['schemas']['PatientAdmission']

export async function fetchBedWardPatients(): Promise<BedWardPatientListItem[]> {
  const response = await fetch(getApiUrl('/api/bed-ward/patients'))
  if (!response.ok) {
    throw new Error('Network response was not ok')
  }
  return response.json()
}

export async function fetchBedWardPatient(patientId: string): Promise<BedWardPatient> {
  const response = await fetch(getApiUrl(`/api/bed-ward/patients/${patientId}`))
  if (!response.ok) {
    throw new Error('Network response was not ok')
  }
  return response.json()
}

export async function admitPatient(admission: PatientAdmission): Promise<void> {
  const response = await fetch(getApiUrl('/api/bed-ward/patients/admit'), {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(admission)
  })
  if (!response.ok) {
    throw new Error('Network response was not ok')
  }
}
