// const baseUrl = 'https://localhost:7197/users'
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
