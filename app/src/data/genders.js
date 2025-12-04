export const genders = [
  {
    value: 0,
    name: 'Male',
  },
  {
    value: 1,
    name: 'Female',
  },
  {
    value: 2,
    name: 'Non-binary',
  },
  {
    value: 3,
    name: 'Decline to state',
  },
]

export const getGenderName = (value) => genders.find((x) => x.value === value)?.name
