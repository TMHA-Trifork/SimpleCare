import { components } from '../types/api-types'
import { getApiUrl } from './config'

export type EmergencyPatientListItem = components['schemas']['EmergencyPatientListItem']
export type EmergencyPatient = components['schemas']['EmergencyPatient']
export type EmergencyRegistration = components['schemas']['EmergencyRegistration']
export type TransferRequest = components['schemas']['TransferRequest']

export async function fetchEmergencyPatients(): Promise<EmergencyPatientListItem[]> {
  const response = await fetch(getApiUrl('/api/emergency-wards/patients'))
  if (!response.ok) {
    throw new Error('Network response was not ok')
  }
  return response.json()
}

export async function fetchEmergencyPatient(patientId: string): Promise<EmergencyPatient> {
  const response = await fetch(getApiUrl(`/api/emergency-wards/patients/${patientId}`))
  if (!response.ok) {
    throw new Error('Network response was not ok')
  }
  return response.json()
}

export async function registerEmergencyPatient(registration: EmergencyRegistration): Promise<void> {
  const response = await fetch(getApiUrl('/api/emergency-wards/patients/register'), {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(registration)
  })
  if (!response.ok) {
    throw new Error('Network response was not ok')
  }
}

export async function transferEmergencyPatient(transfer: TransferRequest): Promise<void> {
  const response = await fetch(getApiUrl('/api/emergency-wards/patients/transfer'), {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(transfer)
  })
  if (!response.ok) {
    throw new Error('Network response was not ok')
  }
}
