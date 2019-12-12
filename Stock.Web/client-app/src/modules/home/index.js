// actions types
const HOME_LOADED = "HOME/LOADED"

//reducer
const initialState = {
    loading: true
}

export default function reducer(state = initialState, action = {}) {
    switch (action.type) {
        case HOME_LOADED:
            return { ...state, loading: false }
        default:
            return { ...state }
    }
}

// actions creators
export const load = () => dispatch => {
    setTimeout(() => dispatch({ type: HOME_LOADED, payload: null }), 1500)
}
