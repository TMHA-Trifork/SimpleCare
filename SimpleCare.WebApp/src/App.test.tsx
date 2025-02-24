import { render, screen } from '@testing-library/react'
import App from './App'

test('renders Simpel care text', () => {
  render(<App />)
  const linkElement = screen.getByText(/Simpel care/i)
  expect(linkElement).toBeInTheDocument()
})
