import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import App from './App';

beforeEach(() => {
  jest.spyOn(global, 'fetch');
});

afterEach(() => {
  jest.restoreAllMocks();
});

describe('Commission Calculator App', () => {

  test('renders app title', () => {
    render(<App />);

    expect(
      screen.getByText(/Commission Calculator/i)
    ).toBeInTheDocument();
  });

  test('allows user to enter sales values', () => {
    render(<App />);

    fireEvent.change(screen.getByLabelText(/Local Sales Count/i), {
      target: { value: '10' }
    });

    fireEvent.change(screen.getByLabelText(/Foreign Sales Count/i), {
      target: { value: '5' }
    });

    fireEvent.change(screen.getByLabelText(/Average Sale Amount/i), {
      target: { value: '1000' }
    });

    expect(screen.getByLabelText(/Local Sales Count/i).value).toBe('10');
    expect(screen.getByLabelText(/Foreign Sales Count/i).value).toBe('5');
    expect(screen.getByLabelText(/Average Sale Amount/i).value).toBe('1000');
  });

  test('calls API and displays commission results', async () => {
    fetch.mockResolvedValueOnce({
      ok: true,
      json: async () => ({
        avalphaTechnologiesCommissionAmount: 3750,
        competitorCommissionAmount: 577.5
      })
    });

    render(<App />);

    fireEvent.change(screen.getByLabelText(/Local Sales Count/i), {
      target: { value: '10' }
    });

    fireEvent.change(screen.getByLabelText(/Foreign Sales Count/i), {
      target: { value: '5' }
    });

    fireEvent.change(screen.getByLabelText(/Average Sale Amount/i), {
      target: { value: '1000' }
    });

    fireEvent.click(
      screen.getByRole('button', { name: /Calculate Commission/i })
    );

    await waitFor(() => {
      expect(screen.getByText('£3750.00')).toBeInTheDocument();
      expect(screen.getByText('£577.50')).toBeInTheDocument();
    });

    expect(fetch).toHaveBeenCalledTimes(1);
  });

});
