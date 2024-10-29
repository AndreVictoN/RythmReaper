/**
 * @file ge_public.h
 * @brief Reactional API
 * @copyright Copyright © 2022 Gestrument AB. All rights reserved.
 *
 * @section functions Functions
 * @subsection functions_setup Setup
 *
 * - reactional_new()
 *   - Create a new engine instance.
 * - reactional_free()
 *   - Free an engine instance.
 * - reactional_add_track_from_path()
 *   - Add a track from a file path on disk.
 * - reactional_add_track_from_string()
 *   - Add a track from a buffer in memory.
 * - reactional_get_num_tracks()
 *   - Get the number of tracks in the engine.
 * - reactional_set_track()
 *   - Set the engine track.
 * - reactional_set_theme()
 *   - Set the engine theme.
 * - reactional_get_track()
 *   - Get the current track.
 * - reactional_get_theme()
 *   - Get the current theme.
 *
 * @subsection functions_assets Assets
 *
 * - reactional_get_num_assets()
 *   - Get the total number of assets.
 * - reactional_get_asset_id()
 *   - Get the ID of an asset.
 * - reactional_get_asset_type()
 *   - Get the type of an asset.
 * - reactional_get_asset_uri()
 *   - Get the URI of an asset.
 * - reactional_set_asset_data()
 *   - Set asset data.
 *
 * @subsection functions_process Process
 *
 * - reactional_process()
 *   - Main process function.
 * - reactional_render_planar()
 *   - Audio process function in using planar buffer layout.
 * - reactional_render_interleaved()
 *   - Audio process function in using interleaved buffer layout.
 *
 * @subsection functions_events Events
 *
 * - reactional_event_push()
 *   - Push OSC data.
 * - reactional_event_poll_begin()
 *   - Start polling for OSC events.
 * - reactional_event_poll()
 *   - Dequeue OSC event data.
 * - reactional_event_poll_end()
 *   - Stop polling for events.
 *
 * @subsection functions_parameters Parameters
 *
 * - reactional_get_num_params()
 *   - Get the number of parameters.
 * - reactional_get_param_type()
 *   - Get a parameter type.
 * - reactional_get_param_name()
 *   - Get a parameter name.
 * - reactional_find_param()
 *   - Find a parameter based on its name.
 * - reactional_get_param_bool()
 *   - Get a bool value from a parameter.
 * - reactional_set_param_bool()
 *   - Set a bool value for a parameter.
 * - reactional_get_param_int()
 *   - Get an integer value from a parameter.
 * - reactional_set_param_int()
 *   - Set an integer value for a parameter.
 * - reactional_get_param_float()
 *   - Get a float value from a parameter.
 * - reactional_set_param_float()
 *   - Set a float value for a parameter.
 * - reactional_param_trig()
 *   - Send a trig to a parameter.
 *
 * @subsection functions_interaction Interaction
 *
 * - reactional_get_num_controls()
 *   - Get the number of user controls.
 * - reactional_get_control_name()
 *   - Get a control name.
 * - reactional_get_control_description()
 *   - Get a control description.
 * - reactional_find_control()
 *   - Find a control based on its name.
 * - reactional_get_control_value()
 *   - Get a control value (0.0 - 1.0).
 * - reactional_set_control_value()
 *   - Set a control value (0.0 - 1.0).
 *
 * @subsection functions_stingers Stingers
 *
 * - reactional_get_num_stingers()
 *   - Get the number of stingers.
 * - reactional_stinger_start()
 *   - Start a stinger.
 *
 * @subsection functions_time Time
 *
 * - reactional_get_next_quant_beat()
 *   - Get the next quantized musical beat.
 * - reactional_get_next_quant_time()
 *   - Get the next quantized time.
 * - reactional_get_next_quant_frames()
 *   - Get the next quantized audio frames.
 * - reactional_get_beats_from_time()
 *   - Convert time to beats.
 * - reactional_get_beats_from_frames()
 *   - Convert frames to beats.
 * - reactional_get_time_from_beats()
 *   - Convert beats to time.
 * - reactional_get_time_from_frames()
 *   - Convert frames to time.
 * - reactional_get_frames_from_beats()
 *   - Convert beats to frames.
 * - reactional_get_frames_from_time()
 *   - Convert time to frames.
 *
 * @subsection functions_osc OSC
 *
 * - reactional_osc_message_encode()
 *   - Encode an OSC message.
 * - reactional_osc_message_decode()
 *   - Decode an OSC message.
 *
 * @subsection functions_utils Utils
 *
 * - reactional_string_error()
 *   - Convert an error code to a human readable string representation.
 * - reactional_set_log_callback()
 *   - Set a log callback function for the built-in logger.
 * - reactional_set_log_level()
 *   - Set the log level.
 * - reactional_get_version()
 *   - Get the library version.
 * - reactional_get_git_revision()
 *   - Get the git revision hash.
 * - reactional_get_build_type()
 *   - Get the library build type.
 * - reactional_get_script_support()
 *   - Get the supported scripting languages of the library.
 */

#ifndef REACTIONAL_PUBLIC_H
#define REACTIONAL_PUBLIC_H

#include <stdint.h>
#include <stdbool.h>

/**
 * @def REACTIONAL_PUB
 * @brief Ensure external linkage visibility.
 */
#ifdef GE_TIMELINE_BUILD_LIB
#if _WIN32
#define REACTIONAL_PUB __declspec(dllexport)
#else
#define REACTIONAL_PUB __attribute__ ((visibility ("default")))
#endif
#else
#define REACTIONAL_PUB
#endif

/**
 * @brief Callback for logging.
 */
typedef int (*reactional_log_callback_func)(const char *message, int size);

/**
 * @brief Used for encoding/decoding OSC data.
 */
typedef union reactional_osc_value_ reactional_osc_value;

/**
 * @brief OSC value where each member represents an OSC type.
 */
union reactional_osc_value_
{

  /**
   * @brief Used by the typetag type 'i'.
   */
  int32_t i;

  /**
   * @brief Used by the typetag type 'f'.
   */
  float f;

  /**
   * @brief Used by the typetag type 's' and 'S'.
   */
  struct
  {

    /**
     * @brief The size of the string, excluding 0 terminator.
     */
    int32_t size;

    /**
     * @brief A pointer to the string in some OSC data.
     */
    const char *s;

  } s, S;

  /**
   * @brief Used by the typetag type 'b'.
   */
  struct
  {

    /**
     * @brief The size of the blob.
     */
    int32_t size;

    /**
     * @brief A pointer to the blob data in some OSC data.
     */
    const void *b;

  } b;

  /**
   * @brief Used by the typetag type 'h'.
   */
  int64_t h;

  /**
   * @brief Used by the typetag type 't'.
   */
  uint64_t t;

  /**
   * @brief Used by the typetag type 'd'.
   */
  double d;

  /**
   * @brief Used by the typetag type 'c'.
   */
  char c;

  /**
   * @brief Used by the typetag type 'r'.
   */
  uint32_t r;

  /**
   * @brief Used by the typetag type 'm'.
   */
  uint8_t m[4];

  /**
   * @brief Used by typetags 'T' and 'F' when decoded, ignored when encoding.
   */
  bool TF;

};

/**
 * @brief Stinger behaviours.
 */
enum
{
  REACTIONAL_STINGER_BEHAVIOUR_OVERLAY,
  REACTIONAL_STINGER_BEHAVIOUR_IGNORE,
  REACTIONAL_STINGER_BEHAVIOUR_STOP,
};

/**
 * @brief Engine parameters.
 */
enum
{

  /**
   * @brief The engine sample rate, will be used for all tracks.
   */
  REACTIONAL_PARAM_SAMPLE_RATE,

  /**
   * @brief The engine block, will be used for all tracks.
   */
  REACTIONAL_PARAM_BLOCK_SIZE,

  /**
   * @brief The current time for the engine in microseconds.
   */
  REACTIONAL_PARAM_CURRENT_TIME,

  /**
   * @brief The gain multiplier for the track.
   */
  REACTIONAL_PARAM_TRACK_GAIN,

  /**
   * @brief The gain multiplier for the theme.
   */
  REACTIONAL_PARAM_THEME_GAIN,

  /**
   * @brief ID of the current track.
   */
  REACTIONAL_PARAM_CURRENT_TRACK,

  /**
   * @brief ID of the current theme.
   */
  REACTIONAL_PARAM_CURRENT_THEME,

  /**
   * @brief Lookhead time for events.
   */
  REACTIONAL_PARAM_LOOKAHEAD,

  /**
   * @brief Maximum enumeration.
   */
  MAX_REACTIONAL_PARAM

};

#ifdef __cplusplus
extern "C" {
#endif

// Setup

/**
 * @brief Create a new reactional engine instance.
 * @return An opaque pointer on success or NULL if out of memory.
 */
REACTIONAL_PUB void *reactional_new(void);

/**
 * @brief Free a reactional engine.
 * @param engine The engine to free.
 */
REACTIONAL_PUB void reactional_free(void *engine);

/**
 * @brief Reset the engine and all of it's tracks to their original state.
 * @param engine The engine.
 */
REACTIONAL_PUB void reactional_reset(void *engine);

/**
 * @brief Reset a track to it's original state.
 * @param engine The engine.
 * @param id The track ID.
 * @returns 0 on success or @ref GE_TIMELINE_ERROR_NOENT if @p id is invalid.
 * @note This will set the track status to stopped.
 */
REACTIONAL_PUB int reactional_reset_track(void *engine, int id);

/**
 * @brief Add a reactional track from a file path.
 * @param engine The engine.
 * @param path An absolute path to a track format.
 * @param key Decryption key.
 * @param key_size The size in bytes of @p key or -1 for auto-detect key in file.
 * @return A track ID or a negative error code.
 */
REACTIONAL_PUB int reactional_add_track_from_path(void *engine, const char *path, const void *key, int key_size);

/**
 * @brief Add a reactional track using a track format string.
 * @param engine The engine.
 * @param json_str The track format JSON.
 * @param size Size of @p json_str excluding the zero terminator.
 * @param key Decryption key.
 * @param key_size The size in bytes of @p key or -1 for auto-detect key in json_str.
 * @return A track ID or a negative error code.
 */
REACTIONAL_PUB int reactional_add_track_from_string(void *engine, const char *json_str, int size, const void *key, int key_size);

/**
 * @brief Update a previously added track.
 * @param engine The engine.
 * @param track_id The track to update.
 * @param json_str The track model to update @p track_id with.
 * @return 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_track_update(void *engine, int track_id, const char *json_str);

/**
 * @brief Validate an encrypted track.
 * @param encrypted_json The data to validate.
 * @param encrypted_size Size of @p encrypted_json excluding the zero terminator.
 * @param key Decryption key.
 * @param key_size The size in bytes of @p key or -1 for auto-detect key in encrypted_json.
 * @param[out] buffer If non-NULL store unencrypted json data here. The buffer must be at least @p size bytes.
 * @param buffer_size Store at most this many bytes in @p buffer, must be at least @p encrypted_size + 1 bytes
 * or the JSON string will get truncated.
 * @return 0 on success or a negative error code.
 * @ref GE_TIMELINE_ERROR_TIMESTAMP if the timestamp was invalid.
 * @ref GE_TIMELINE_ERROR_SERIAL if the JSON could not be parsed.
 * @ref GE_TIMELINE_ERROR_NOMEM if @p buffer was too small.
 */
REACTIONAL_PUB int reactional_validate_track(const char *encrypted_json, int encrypted_size, const void *key, int key_size, char *buffer, int buffer_size);

/**
 * @brief Get the number of added tracks.
 * @param engine The engine.
 * @return The number of tracks in the Engine.
 */
REACTIONAL_PUB int reactional_get_num_tracks(const void *engine);

/**
 * @brief Remove a track from the engine.
 * @param engine The engine.
 * @param id The track ID.
 * @returns 0 on success or a negative error code on failure.
 */
REACTIONAL_PUB int reactional_remove_track(void *engine, int id);

/**
 * @brief Delete a track freeing its resources.
 * @param engine The engine.
 * @param id The id of the track to get.
 * @param ret If non-NULL write the return value here.
 */
// REACTIONAL_PUB void reactional_delete(void *engine, int id, int *ret);

/**
 * @brief Set the current track that will be processed and played back.
 * @param engine The engine.
 * @return 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_set_track(void *engine, int id);

/**
 * @brief Set the current theme that will be processed and played back.
 * @param engine The engine.
 * @return 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_set_theme(void *engine, int id);

/**
 * @brief Unset the current track.
 * @param engine The engine.
 * @return 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_unset_track(void *engine);

/**
 * @brief Unset the current theme.
 * @param engine The engine.
 * @return 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_unset_theme(void *engine);

/**
 * @brief Get the current track ID.
 * @param engine The engine.
 * @return The track ID or a negative error code if no track is currently set.
 */
REACTIONAL_PUB int reactional_get_track(const void *engine);

/**
 * @brief Get the current theme ID.
 * @param engine The engine.
 * @return The theme ID or a negative error code if no theme is currently set.
 */
REACTIONAL_PUB int reactional_get_theme(const void *engine);

// Assets

/**
 * @brief Get the number of assets.
 * @param engine The engine.
 * @param id The track ID.
 * @return The number of assets.
 */
REACTIONAL_PUB int reactional_get_num_assets(void *engine, int id);

/**
 * @brief Get the ID of an asset.
 * @param engine The engine.
 * @param id The track ID.
 * @param index The index of the asset.
 * @returns The asset ID or an empty string if @p index was out-of-range.
 */
REACTIONAL_PUB const char *reactional_get_asset_id(void *engine, int id, int index);

/**
 * @brief Get the URI of an asset.
 * @param engine The engine.
 * @param id The track ID.
 * @param index The index of the asset.
 * @returns The asset URI or an empty string if @p index was out-of-range.
 */
REACTIONAL_PUB const char *reactional_get_asset_uri(void *engine, int id, int index);

/**
 * @brief Get the type of an asset.
 * @param engine The engine.
 * @param id The track ID.
 * @param index The index of the asset.
 * @returns The asset type or an empty string if @p index was out-of-range.
 */
REACTIONAL_PUB const char *reactional_get_asset_type(void *engine, int id, int index);

/**
 * @brief Set the data of an asset.
 * @param engine The engine.
 * @param id The track ID.
 * @param asset_id The asset ID.
 * @param asset_type A type string describing @p data.
 * @param asset_data The data to set.
 * @param asset_size The size in bytes of @p data.
 * @param key Decryption key.
 * @param key_size The size in bytes of @p key.
 * @return 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_set_asset_data
(
  void *engine,
  int id,
  const char *asset_id,
  const char *asset_type,
  const void *asset_data,
  int asset_size,
  const void *key,
  int key_size
);

// Process

/**
 * @brief Process the reactional engine. This function should only be called from a main thread context.
 * @param track The track to process.
 * @param system_time Number of microseconds to advance the time or -1 to use the interal clock.
 * @returns 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_process(void *engine, int64_t system_time);

// Audio

/**
 * @brief Render audio buffers for the track in a planar channel layout.
 * @param engine The engine.
 * @param sample_rate The sample rate given by the audio host process function.
 * @param num_frames The number of frames given by the host audio process function.
 * @param num_channels The number of channels given by the host audio process function.
 * @param buffers Store the samples for the audio here.
 * @returns 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_render_planar(void *engine, double sample_rate, int num_frames, int num_channels, float **buffers);

/**
 * @brief Render audio buffers for the track in an interleaved channel layout.
 * @param engine The engine.
 * @param sample_rate The sample rate given by the audio host process function.
 * @param num_frames The number of frames given by the host audio process function.
 * @param num_channels The number of channels given by the host audio process function.
 * @param buffer Store the samples for the audio here.
 * @returns 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_render_interleaved(void *engine, double sample_rate, int num_frames, int num_channels, float *buffer);

#ifdef __EMSCRIPTEN__
REACTIONAL_PUB int reactional_render_emscripten(void *engine, double sample_rate, int num_frames, int num_channels, float *buffer);
#endif

// Events

/**
 * @brief Push an OSC message into the track input queue.
 * @param engine The engine.
 * @param id The track ID.
 * @param microbeats Schedule the OSC event for this absolute microbeat.
 * @param osc The OSC data.
 * @param size Read at most his many bytes from @p osc.
 * @returns The number of consumed bytes on success or a negative error code on failure.
 */
REACTIONAL_PUB int reactional_event_push(void *engine, int id, int64_t microbeats, const void *osc, int size);

/**
 * @brief Start polling events from the track.
 * @param engine The engine.
 * @param id The track ID (currently not used).
 * @param[out] start If non-NULL store the track clock current beat here.
 * @returns The number of events available for reading.
 */
REACTIONAL_PUB int reactional_event_poll_begin(void *engine, int id, int64_t *start_beat);

/**
 * @brief Poll generated OSC events from the track.
 * @param engine The engine.
 * @param target -1 for all events. 0 for track events and 1 for theme events.
 * @param index The event index.
 * @param size If non-NULL and the function return value is non-NULL, store the number of bytes for the OSC data here.
 * @returns Pointer to the OSC data or NULL if there were no events available
 * for the specified index.
 * @note The OSC data is only valid until reactional_event_poll_end() is called.
 */
REACTIONAL_PUB const void *reactional_event_poll(const void *engine, int target, int index, int *size);

/**
 * @brief Poll a pointer to the actual event struct memory which can be accessed through
 * the reactional_evstruct_* family of functions.
 * @param engine The engine.
 * @param index The event index.
 * @returns Pointer to the event struct memory or NULL if there were no events available
 * for the specified index.
 * @note The pointer is only valid until reactional_event_poll_end() is called.
 */
REACTIONAL_PUB const void *reactional_event_poll_struct(const void *engine, int index);

/**
 * @brief Stop polling events from the track.
 * @param engine The engine.
 * @param id The track ID (currently not used).
 * @param num_events The number of events polled.
 * @returns The number of ended events.
 */
REACTIONAL_PUB int reactional_event_poll_end(void *engine, int id, int num_events);

// Introspection

/**
 * @brief Get the number of parameters for a track.
 * @param engine The engine.
 * @param id The track ID.
 * @returns The number of parameters.
 */
REACTIONAL_PUB int reactional_get_num_params(const void *engine, int id);

/**
 * @brief Get the parameter type.
 * @param engine The engine.
 * @param id The track ID.
 * @param param_index The index of the parameter.
 * @returns The parameter type or a negative error code.
 */
REACTIONAL_PUB int reactional_get_param_type(const void *engine, int id, int param_index);

/**
 * @brief Get the name of the parameter.
 * @param engine The engine.
 * @param id The track ID.
 * @param param_index The index of the parameter.
 * @returns The parameter name or an empty string if not found.
 */
REACTIONAL_PUB const char *reactional_get_param_name(const void *engine, int id, int param_index);

/**
 * @brief Find the index of a parameter.
 * @param engine The engine.
 * @param id The track ID.
 * @param param_name The param name to find.
 * @returns The index of the parameter or -1 if not found.
 */
REACTIONAL_PUB int reactional_find_param(const void *engine, int id, const char *param_name);

/**
 * @brief Get the parameter value.
 * @param engine The engine.
 * @param id The track ID.
 * @param param_index The param index.
 * @param[out] value If non-NULL and the function returns 0 the value is stored here.
 * @returns 0 on success or a negative error code on failure.
 */
REACTIONAL_PUB int reactional_get_param_bool(const void *engine, int id, int param_index, bool *value);

/**
 * @brief Set the parameter value.
 * @param engine The engine.
 * @param id The track ID.
 * @param param_index The param index.
 * @param value The value to set.
 * @returns 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_set_param_bool(void *engine, int id, int param_index, bool value);

/**
 * @brief Get the parameter value.
 * @param engine The engine.
 * @param id The track ID.
 * @param param_index The param index.
 * @param[out] value If non-NULL and the function returns 0 the value is stored here.
 * @returns 0 on success or a negative error code on failure.
 */
REACTIONAL_PUB int reactional_get_param_int(const void *engine, int id, int param_index, int64_t *value);

/**
 * @brief Set the parameter value.
 * @param engine The engine.
 * @param id The track ID.
 * @param param_index The param index.
 * @param value The value to set.
 * @returns 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_set_param_int(void *engine, int id, int param_index, int64_t value);

/**
 * @brief Get the parameter value.
 * @param engine The engine.
 * @param id The track ID.
 * @param param_index The param index.
 * @param[out] value If non-NULL and the function returns 0 the value is stored here.
 * @returns 0 on success or a negative error code on failure.
 */
REACTIONAL_PUB int reactional_get_param_float(const void *engine, int id, int param_index, double *value);

/**
 * @brief Set the parameter value.
 * @param engine The engine.
 * @param id The track ID.
 * @param param_index The param index.
 * @param value The value to set.
 * @returns 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_set_param_float(void *engine, int id, int param_index, double value);

/**
 * @brief Get the parameter value.
 * @param engine The engine.
 * @param id The track ID.
 * @param param_index The param index.
 * @param[out] value If non-NULL and the function returns 0 the value is stored here.
 * @param n Store at most this many bytes to @p value.
 * @returns The length of the string on success or a negative error code on failure.
 */
REACTIONAL_PUB int reactional_get_param_string(const void *engine, int id, int param_index, char *value, int n);

/**
 * @brief Set the parameter value.
 * @param engine The engine.
 * @param id The track ID.
 * @param param_index The param index.
 * @param value The value to set.
 * @returns The length of the string on success or a negative error code on failure.
 */
REACTIONAL_PUB int reactional_set_param_string(void *engine, int id, int param_index, const char *value);

/**
 * @brief Send a trig to a parameter.
 * @param engine The engine.
 * @param id The track ID.
 * @param param_index The param index.
 * @returns 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_param_trig(const void *engine, int id, int param_index);

// Interaction

/**
 * @brief Get the number of controls for a track.
 * @param engine The engine.
 * @param id The track ID.
 * @returns The number of controls.
 */
REACTIONAL_PUB int reactional_get_num_controls(const void *engine, int id);

/**
 * @brief Get the name of a control.
 * @param engine The engine.
 * @param id The track ID.
 * @param control_index The index of the control.
 * @returns The control name or an empty string if not found.
 */
REACTIONAL_PUB const char *reactional_get_control_name(const void *engine, int id, int control_index);

/**
 * @brief Get the description of a control.
 * @param engine The engine.
 * @param id The track ID.
 * @param control_index The index of the control.
 * @returns The control description or an empty string if not found.
 */
REACTIONAL_PUB const char *reactional_get_control_description(const void *engine, int id, int control_index);

/**
 * @brief Get the type of a control.
 * @param engine The engine.
 * @param id The track ID.
 * @param control_index The index of the control.
 * @returns The control type or an empty string if not found.
 */
REACTIONAL_PUB const char *reactional_get_control_type(const void *engine, int id, int control_index);

/**
 * @brief Get the level of a control.
 * @param engine The engine.
 * @param id The track ID.
 * @param control_index The index of the control.
 * @returns The control level or an empty string if not found.
 */
REACTIONAL_PUB const char *reactional_get_control_level(const void *engine, int id, int control_index);

/**
 * @brief Answer if the control is resettable.
 * @param engine The engine.
 * @param id The track ID.
 * @param control_index The index of the control.
 * @return True if resettable otherwise false.
 */
REACTIONAL_PUB bool reactional_get_control_reset(const void *engine, int id, int control_index);

/**
 * @brief Find the index of a control.
 * @param engine The engine.
 * @param id The track ID.
 * @param control_name The param name to find.
 * @returns The index of the control or -1 if not found.
 */
REACTIONAL_PUB int reactional_find_control(const void *engine, int id, const char *control_name);

/**
 * @brief Get the control value (0 - 1 or a negative value if @p id or @p control_index is invalid).
 * @param engine The engine.
 * @param id The track ID.
 * @param control_index The control index.
 * @param[out] value If non-NULL and the function returns 0 the value is stored here.
 * @returns The value on success or a negative value if @p id or @p control_index is invalid.
 */
REACTIONAL_PUB double reactional_get_control_value(const void *engine, int id, int control_index);

/**
 * @brief Set the control value (0 - 1).
 * @param engine The engine.
 * @param id The track ID.
 * @param control_index The control index.
 * @param value The value to set.
 * @returns 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_set_control_value(void *engine, int id, int control_index, double value);

/**
 * @brief Set a control value array.
 * @param engine The engine.
 * @param id The track ID.
 * @param control_index The control index.
 * @param values The values to set.
 * @param num_values Number of values in @p values.
 * @returns 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_set_control_value_array(void *engine, int id, int control_index, const double *values, int num_values);

/**
 * @brief Get a control value array.
 * @param engine The engine.
 * @param id The track ID.
 * @param control_index The control index.
 * @param values If non-NULL store the values here.
 * @param num_values The number of values to get.
 * @returns 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_get_control_value_array(void *engine, int id, int control_index, double *values, int num_values);

/**
 * @brief Get the length of the value array.
 * @param engine The engine.
 * @param id The track ID.
 * @param control_index The control index.
 * @return The size of the value array or a negative error code.
 */
REACTIONAL_PUB int reactional_get_control_value_array_size(void *engine, int id, int control_index);

/**
 * @brief Reset a control.
 * @param engine The engine.
 * @param id The track ID.
 * @param control_index The control index.
 * @returns 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_reset_control(void *engine, int id, int control_index);

// Stingers

/**
 * @brief Get the number of stingers.
 * @param engine The engine.
 * @param id The track ID.
 * @return The number of stingers in the track.
 */
REACTIONAL_PUB int reactional_get_num_stingers(const void *engine, int id);

/**
 * @brief Get the stinger pickup offset.
 * @param engine The engine.
 * @param id The track ID.
 * @param stinger_index The index of the stinger.
 * @return The offset of the pickup relative from the stinger start.
 */
REACTIONAL_PUB int64_t reactional_get_stinger_pickup(const void *engine, int id, int stinger_index);

/**
 * @brief Start a stinger.
 * @param engine The engine.
 * @param id The track ID.
 * @param stinger_index The index of the stinger to start.
 * @param start_offset An absolute offset in microbeats when to begin playing the stinger.
 * @param behaviour How to behave when re-triggering a stinger.
 * @return 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_stinger_start(void *engine, int id, int stinger_index, int64_t start_offset, int behaviour);

// Parts

/**
 * @brief Get the number of parts in a track.
 * @param engine The engine.
 * @param id The track ID.
 * @return The number of parts in the track.
 */
REACTIONAL_PUB int reactional_get_num_parts(const void *engine, int id);

/**
 * @brief Get the current part index at runtime.
 * @param engine The engine.
 * @param id The track ID.
 * @return The part index or a negative error code.
 */
REACTIONAL_PUB int reactional_get_current_part(const void *engine, int id);

/**
 * @brief Get the name of a part in a track.
 * @param engine The engine.
 * @param id The track ID.
 * @param section_index The part index.
 * @return The part name.
 */
REACTIONAL_PUB const char *reactional_get_part_name(const void *engine, int id, int part_index);

/**
 * @brief Get the beat offset for a part in a track.
 * @param engine The engine.
 * @param id The track ID.
 * @param part_index The part index.
 * @return The offset in beats.
 */
REACTIONAL_PUB int64_t reactional_get_part_offset(const void *engine, int id, int part_index);

/**
 * @brief Get the beat duration for a part in a track.
 * @param engine The engine.
 * @param id The track ID.
 * @param part_index The part index.
 * @return The duration in beats.
 */
REACTIONAL_PUB int64_t reactional_get_part_duration(const void *engine, int id, int part_index);

// Bars

/**
 * @brief Get the number of bars in a track.
 * @param engine The engine.
 * @param id The track ID.
 * @return The number of bars in the track.
 */
REACTIONAL_PUB int reactional_get_num_bars(const void *engine, int id);

/**
 * @brief Get the current bar index at runtime.
 * @param engine The engine.
 * @param id The track ID.
 * @return The bar index or a negative error code.
 */
REACTIONAL_PUB int reactional_get_current_bar(const void *engine, int id);

/**
 * @brief Get the beat offset for a bar in a track.
 * @param engine The engine.
 * @param id The track ID.
 * @param bar_index The bar index.
 * @return The offset in beats.
 */
REACTIONAL_PUB int64_t reactional_get_bar_offset(const void *engine, int id, int bar_index);

/**
 * @brief Get the beat duration for a bar in a track.
 * @param engine The engine.
 * @param id The track ID.
 * @param bar_index The bar index.
 * @return The duration in beats.
 */
REACTIONAL_PUB int64_t reactional_get_bar_duration(const void *engine, int id, int bar_index);

// Snapshots

/**
 * @brief Get the number of states.
 * @param engine The engine.
 * @param id The track ID.
 * @return The number of states.
 */
REACTIONAL_PUB int reactional_get_num_states(void *engine, int id);

/**
 * @brief Find a state from a name.
 * @param engine The engine.
 * @param id The track ID.
 * @param name The name to search for.
 * @return The state index or @ref GE_TIMELINE_ERROR_NOENT if the @p id is
 * invalid, or @ref GE_TIMELINE_ERROR_VALUE @p name was not found.
 */
REACTIONAL_PUB int reactional_find_state(void *engine, int id, const char *name);

/**
 * @brief Set a state.
 * @param engine The engine.
 * @param id The track ID.
 * @param index The state index to set.
 * @param lag_multiplier A lag multiplier for the control values. If < 0 the default will be used.
 * @return 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_set_state(void *engine, int id, int state_index, double lag_multiplier);

/**
 * @brief Get the number of snapshots.
 * @param engine The engine.
 * @param id The track ID.
 * @return The number of snapshots.
 */
REACTIONAL_PUB int reactional_get_num_snapshots(void *engine, int id);

/**
 * @brief Find a snapshot from a name.
 * @param engine The engine.
 * @param id The track ID.
 * @param name The name to search for.
 * @return The snapshot index or @ref GE_TIMELINE_ERROR_NOENT if the @p id is
 * invalid, or @ref GE_TIMELINE_ERROR_VALUE @p name was not found.
 */
REACTIONAL_PUB int reactional_find_snapshot(void *engine, int id, const char *name);

/**
 * @brief Set a snapshot.
 * @param engine The engine.
 * @param id The track ID.
 * @param snapshot_index The snapshot index to set.
 * @param lag_multiplier A lag multiplier for the control values. If < 0 the default will be used.
 * @return 0 on success or a negative error code.
 */
REACTIONAL_PUB int reactional_set_snapshot(void *engine, int id, int snapshot_index, double lag_multiplier);

// Utils

/**
 * @brief Get a quantized beat into the future. This function can be
 * used to calculate musical timing for ad-hoc events.
 * @param engine The engine.
 * @param id The track ID.
 * @param quant The quantization in beats.
 * @param phase The phase in beats.
 * @returns The quantized beat.
 * @note @p quant and @p phase can be used to find the next `n` beat in a bar,
 * if quant is 4000000 and phase is 1000000 the function will return the beat position of
 * the 2nd beat in the next bar (if the bar is 4000000 beats long).
 */
REACTIONAL_PUB int64_t reactional_get_next_quant_beat
(
  const void *engine,
  int id,
  int64_t quant,
  int64_t phase
);

/**
 * @brief Get a quantized beat into the future. This function can be
 * used to calculate musical timing for ad-hoc events.
 * @param engine The engine.
 * @param id The track ID.
 * @param quant The quantization in beats.
 * @param phase The phase in beats.
 * @param relative If true, return the relative offset otherwise absolute.
 * @returns The quantized beat in microseconds.
 * @note @p quant and @p phase can be used to find the next `n` beat in a bar,
 * if quant is 4000000 and phase is 1000000 the function will return the beat position of
 * the 2nd beat in the next bar (if the bar is 4000000 beats long).
 */
REACTIONAL_PUB int64_t reactional_get_next_quant_time
(
  const void *engine,
  int id,
  int64_t quant,
  int64_t phase,
  bool relative
);

/**
 * @brief Get a quantized beat into the future. This function can be
 * used to calculate musical timing for ad-hoc events.
 * @param engine The engine.
 * @param id The track ID.
 * @param quant The quantization in beats.
 * @param phase The phase in beats.
 * @param relative If true, return the relative offset otherwise absolute.
 * @returns The quantized beat in microseconds.
 * @note @p quant and @p phase can be used to find the next `n` beat in a bar,
 * if quant is 4000000 and phase is 1000000 the function will return the beat position of
 * the 2nd beat in the next bar (if the bar is 4000000 beats long).
 */
REACTIONAL_PUB int64_t reactional_get_theme_quant_time
(
  const void *engine,
  int64_t quant,
  int64_t phase
);

/**
 * @brief Get a quantized beat into the future. This function can be
 * used to calculate musical timing for ad-hoc events.
 * @param engine The engine.
 * @param id The track ID.
 * @param quant The quantization in beats.
 * @param phase The phase in beats.
 * @param relative If true, return the relative offset otherwise absolute.
 * @returns The quantized beat in audio frames.
 * @note @p quant and @p phase can be used to find the next `n` beat in a bar,
 * if quant is 4000000 and phase is 1000000 the function will return the beat position of
 * the 2nd beat in the next bar (if the bar is 4000000 beats long).
 */
REACTIONAL_PUB int64_t reactional_get_next_quant_frames
(
  const void *engine,
  int id,
  int64_t quant,
  int64_t phase,
  bool relative
);

/**
 * @brief Convert time to beats.
 * @param engine The engine to do the conversion for.
 * @param id The track ID.
 * @param time The time in seconds.
 * @returns The number of beats for the specified time.
 */
REACTIONAL_PUB double reactional_get_beats_from_time(const void *engine, int id, double time);

/**
 * @brief Convert audio frames to beats.
 * @param engine The engine to do the conversion for.
 * @param id The track ID.
 * @param frames The number of audio frames.
 * @returns The number of beats for the specified number of audio frames.
 */
REACTIONAL_PUB double reactional_get_beats_from_frames(const void *engine, int id, double frames);

/**
 * @brief Convert beats to time.
 * @param engine The engine to do the conversion for.
 * @param id The track ID.
 * @param beats The beats.
 * @returns The time for the specified number of beats.
 */
REACTIONAL_PUB double reactional_get_time_from_beats(const void *engine, int id, double beats);

/**
 * @brief Convert audio frames to time.
 * @param engine The engine to do the conversion for.
 * @param id The track ID.
 * @param frames The number of audio frames.
 * @returns The time for the specified number of audio frames.
 */
REACTIONAL_PUB double reactional_get_time_from_frames(const void *engine, int id, double frames);

/**
 * @brief Convert beats to audio frames.
 * @param engine The engine to do the conversion for.
 * @param id The track ID.
 * @param beats The beats.
 * @returns The number of audio frames from the specified number of beats.
 */
REACTIONAL_PUB double reactional_get_frames_from_beats(const void *engine, int id, double beats);

/**
 * @brief Convert time to audio frames.
 * @param engine The engine to do the conversion for.
 * @param id The track ID.
 * @param time The time in seconds.
 * @returns The number of audio frames from the specified time.
 */
REACTIONAL_PUB double reactional_get_frames_from_time(const void *engine, int id, double time);

/**
 * @brief Start a fade in/out envelope and set the status to stopped when done.
 * @param engine The engine.
 * @param target The target amplitude.
 * @param beat_offset The absolute offset in beats when to start the fade.
 * @param time_duration The duration of the fade in microseconds.
 * @param stop_finish True to stop the track when the fade is finished.
 * @returns 0 on success or @ref GE_TIMELINE_ERROR_NOENT if the engine has no track set.
 */
REACTIONAL_PUB int reactional_track_fade(void *engine, float target, int64_t beat_offset, int64_t time_duration, bool stop_finish);

/**
 * @brief Start a fade in/out envelope and set the status to stopped when done.
 * @param engine The engine.
 * @param target The target amplitude.
 * @param beat_offset The absolute offset in beats when to start the fade.
 * @param time_duration The duration of the fade in microseconds.
 * @param stop_finish True to stop the theme when the fade is finished.
 * @returns 0 on success or @ref GE_TIMELINE_ERROR_NOENT if the engine has no theme set.
 */
REACTIONAL_PUB int reactional_theme_fade(void *engine, float target, int64_t beat_offset, int64_t time_duration, bool stop_finish);

/**
 * @brief Get the string representation of an error code.
 * @param err_code The error to inspect.
 * @returns A human readable string describing the error.
 */
REACTIONAL_PUB const char *reactional_string_error(int err_code);

/**
 * @brief Set a log callback. The default is stdout.
 * @param cb A callback for log output.
 */
REACTIONAL_PUB void reactional_set_log_callback(reactional_log_callback_func cb);

/**
 * @brief Set the log level.
 * @param level The log level.
 *
 * Log levels are as follows:
 *
 * - 0 = Nothing.
 * - 1 = Critical.
 * - 2 = Error.
 * - 3 = Warning.
 * - 4 = Info.
 * - 5 = Debug.
 */
REACTIONAL_PUB void reactional_set_log_level(int level);

/**
 * @brief Get the version of the library.
 * @returns A pointer to the version string formatted as "major.minor.patch".
 */
REACTIONAL_PUB const char *reactional_get_version(void);

/**
 * @brief Get the git revision of the library.
 * @returns A pointer to the git revision string.
 */
REACTIONAL_PUB const char *reactional_get_git_revision(void);

/**
 * @brief Get the build type of the library.
 * @returns A pointer to the build type string, something like "Debug" or "Release".
 */
REACTIONAL_PUB const char *reactional_get_build_type(void);

/**
 * @brief Get the supported scripting languages of the library.
 * @return A pointer to a string of the supported scripting languages.
 */
REACTIONAL_PUB const char *reactional_get_script_support(void);

/**
 * @brief Encode an entire message.
 * @param[out] data If non-NULL store the encoded OSC data here.
 * @param size Store at most this many bytes in @p data.
 * @param address The address to encode.
 * @param address_size The maximum number of bytes to read from @p address.
 * @param typetag An typetag string without the preceeding comma, will check for 0 terminator.
 * @param typetag_size Read at most this many bytes from @p typetag.
 * @param values Encode these values. The length of the
 * @param[out] num_values If non-NULL store the number of encoded values here. The value is
 * stored regardless of the function return value.
 * typetag indicates the number of expected values, capped by @p typetag_size.
 * @return The number of encoded bytes on success or a negative error code.
 */
REACTIONAL_PUB int32_t reactional_osc_message_encode
(
  void *data,
  int32_t size,
  const char *address,
  int32_t address_size,
  const char *typetag,
  int32_t typetag_size,
  const reactional_osc_value *values,
  int32_t *num_values
);

/**
 * @brief Decode an entire message.
 * @param data The OSC data to decode.
 * @param size Read at most this many bytes from @p data.
 * @param[out] address If non-NULL and successful call store the pointer to the address here.
 * @param[out] address_size If non-NULL and successful call store the size of the address here,
 * @param[out] typetag If non-NULL and successful call store the pointer to the typetag here.
 * @param[out] typetag_size If non-NULL and successful call store the size of the typetag here,
 * @param max_values The maximum number of values to store in @p values.
 * @param[out] values If non-NULL store the decoded values here. The length of
 * the typetag indicates the number of expected values, capped by
 * @p typetag_size.
 * @param[out] num_values If non-NULL store the number of decoded values here. The value is
 * stored regardless of the function return value.
 * @return The number of decoded bytes on success or a negative error code.
 */
REACTIONAL_PUB int32_t reactional_osc_message_decode
(
  const void *data,
  int32_t size,
  const char **address,
  int32_t *address_size,
  const char **typetag,
  int32_t *typetag_size,
  int32_t max_values,
  reactional_osc_value *values,
  int32_t *num_values
);

/**
 * @brief Get the number of licenses.
 * @returns The number of licenses.
 */
REACTIONAL_PUB int reactional_get_num_licenses(void);

/**
 * @brief Get the name of a license.
 * @param index The index of the license.
 * @returns The license name or NULL if @p index is out of bounds.
 */
REACTIONAL_PUB const char *reactional_get_license_name(int index);

/**
 * @brief Get the text of a license.
 * @param index The index of the license.
 * @returns The license text or NULL if @p index is out of bounds.
 */
REACTIONAL_PUB const char *reactional_get_license_text(int index);

/**
 * @brief Get channel amp value for a channel in a track.
 * @param engine The engine.
 * @param track_id The track id for the track to set the channel amp for.
 * @param channel The channel to set the amp for.
 * @param[out] amp The amp value is stored here on success.
 * @returns 0 on success or a negative error code.
*/
REACTIONAL_PUB int reactional_get_channel_amp(const void *engine, int track_id, int channel_index, double *amp);

/**
 * @brief Set channel amp value for a channel in a track.
 * @param engine The engine.
 * @param track_id The track id for the track to set the channel amp for.
 * @param channel The channel to set the amp for.
 * @param amp The amp value to set.
 * @returns 0 on success or a negative error code.
*/
REACTIONAL_PUB int reactional_set_channel_amp(void *engine, int track_id, int channel_index, double amp);

#ifdef __EMSCRIPTEN__

/**
 * @brief Schedule quantized track start based on a playing theme.
 * @param engine The engine.
 * @param quant The quantization in beats.
 */
REACTIONAL_PUB int reactional_wasm_schedule_track_start_on_theme(const void *engine, int64_t quant);

/**
 * @brief Schedule quantized theme start based on a playing track.
 * @param engine The engine.
 * @param track_id The track id for the track to start.
 * @param quant The quantization in beats.
 */
REACTIONAL_PUB int reactional_wasm_schedule_theme_start_on_track(const void *engine, int track_id, int64_t quant);

/**
 * @brief Get the OSC value for a specific typetag in a @ref reactional_osc_value union.
 * @param osc_values Pointer to @ref reactional_osc_value objects.
 * @param index Index of the value to get.
 * @param typetag A string representing the typetag for the value to get. Should be a string with a single character.
 * @param[out] size The OSC value data size will be written here on success.
 * @returns A pointer to the OSC value data or NULL on error.
 */
REACTIONAL_PUB const void *reactional_wasm_get_osc_value(const void *osc_values, int index, const char *typetag, int32_t *size);

/**
 * @brief Set channel amp value for a channel in a track.
 * @param engine The engine.
 * @param track_id The track id for the track to set the channel amp for.
 * @param channel The channel to set the amp for.
 * @param amp The amp value to set.
 * @returns 0 on success or a negative error code.
*/
REACTIONAL_PUB const int reactional_wasm_set_channel_amp(const void *engine, int track_id, int channel, float amp);

/* #include <pthread.h>
#include <signal.h>
#include <emscripten/webaudio.h>
REACTIONAL_PUB void *reactional_process_thread_create(void *engine);

REACTIONAL_PUB void *reactional_render_thread_create
(
  void *engine,
  double sample_rate,
  int num_frames,
  int num_channels
);

REACTIONAL_PUB int reactional_process_thread_destroy(void *thread);

REACTIONAL_PUB EMSCRIPTEN_WEBAUDIO_T reactional_create_audio_context(void);

REACTIONAL_PUB void reactional_start_audio_context
(
  EMSCRIPTEN_WEBAUDIO_T context,
  void *engine,
  double sample_rate,
  int num_frames,
  int num_channels
); */

REACTIONAL_PUB void reactional_set_emscripten_log(int level);
#endif

// Opaque event struct functions.

/**
 * @brief Get the event type from event struct memory.
 * @param event A pointer to the event struct memory.
 * @returns The event type.
 * @note We probably only use OSC event types (type 1).
 *
 * The event type is defined in private ge_event.h as enum `ge_timeline_event_type`:
 *
 * - GE_TIMELINE_EVENT_TYPE_NONE: 0
 * - GE_TIMELINE_EVENT_TYPE_OSC: 1
 * - GE_TIMELINE_EVENT_TYPE_LOGIC: 2
 * - GE_TIMELINE_EVENT_TYPE_SEQUENCE: 3
 */
REACTIONAL_PUB int reactional_evstruct_get_type(const void *event);

/**
 * @brief Get the offset in microbeats from event struct memory.
 * @param event A pointer to the event struct memory.
 * @returns The offset in microbeats.
 */
REACTIONAL_PUB int64_t reactional_evstruct_get_offset(const void *event);

/**
 * @brief Get the duration in microbeats from event struct memory.
 * @param event A pointer to the event struct memory.
 * @returns The duration in microbeats.
 */
REACTIONAL_PUB int64_t reactional_evstruct_get_duration(const void *event);

/**
 * @brief Get the lane index from event struct memory.
 * @param event A pointer to the event struct memory.
 * @returns The lane index.
 */
REACTIONAL_PUB int reactional_evstruct_get_lane_index(const void *event);

/**
 * @brief Get the sink index from event struct memory.
 * @param event A pointer to the event struct memory.
 * @returns The sink index.
 */
REACTIONAL_PUB int reactional_evstruct_get_sink_index(const void *event);

/**
 * @brief Get the output index from event struct memory.
 * @param event A pointer to the event struct memory.
 * @returns The output index.
 */
REACTIONAL_PUB int reactional_evstruct_get_output_index(const void *event);

/**
 * @brief Get the priority from event struct memory.
 * @param event A pointer to the event struct memory.
 * @returns The priority (ascending sort, lower value means higher priority).
 */
REACTIONAL_PUB int reactional_evstruct_get_priority(const void *event);

/**
 * @brief Get the theme flag from event struct memory.
 * @param event A pointer to the event struct memory.
 * @returns True if the event came from a theme.
 */
REACTIONAL_PUB bool reactional_evstruct_get_is_theme(const void *event);

#ifdef __cplusplus
}
#endif

#endif /* REACTIONAL_PUBLIC_H */
