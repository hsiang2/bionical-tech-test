// Original template URL (HTTPS):
// const baseUrl = 'https://localhost:7197/users'

// On my machine, the HTTPS endpoint shows the browser error:
// "Failed to load resource: The certificate for this server is invalid."
// So I am using the HTTP endpoint instead:
const baseUrl = 'http://localhost:5212/users'

export const getUsers = async () => {
  try {
    const response = await fetch(baseUrl)
    if (!response.ok) {
      throw new Error(`Response status: ${response.status}`)
    }

    return await response.json()
  } catch (error) {
    console.error(error.message)
    return null
  }
}

export const getUser = async (userId) => {
  try {
    const response = await fetch(`${baseUrl}/${userId}`)
    if (!response.ok) {
      throw new Error(`Response status: ${response.status}`)
    }

    return await response.json()
  } catch (error) {
    console.error(error.message)
  }
}

export const updateUser = async (userId, payload) => {
  const response = await fetch(`${baseUrl}/${userId}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(payload)
  })

  let data = null
  try {
    data = await response.json()
  } catch {
    data = null
  }

  if (!response.ok) {
    const errors = data?.errors
    const message = Array.isArray(errors) && errors.length
      ? errors.join('\n')
      : `Request failed with status ${response.status}`

    throw new Error(message)
  }

  return data
}

